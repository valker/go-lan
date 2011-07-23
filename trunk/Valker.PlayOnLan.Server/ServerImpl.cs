using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.PluginLoader;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Server.Messages.Server;
using Valker.PlayOnLan.Server2008.Messages.Client;

namespace Valker.PlayOnLan.Server2008
{
    public class ServerImpl : IServerMessageExecuter, IDisposable
    {
        private readonly IEnumerable<IGameType> _games = new List<IGameType>(Loader.Load(Environment.CurrentDirectory));

        private readonly IDictionary<string, IGameType> _gameDict = new Dictionary<string, IGameType>();

        // Added when new party registred
        // Removed when party is removed, OR client that register the party is removed
        private readonly List<PartyState> _partyStates = new List<PartyState>();

        private int _partyStateId;

        // Added when new transport is attached to the server
        private readonly List<IMessageConnector> _connectors = new List<IMessageConnector>();

        // Added when new player is registred
        private readonly List<IPlayer> _players = new List<IPlayer>();
        
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public ServerImpl(IEnumerable<IMessageConnector> connectors)
        {
            if (connectors == null) throw new ArgumentNullException("connectors");

            foreach (var game in _games)
            {
                Trace.WriteLine(game.Name);
                _gameDict.Add(game.Id, game);
            }

            foreach (var connector in connectors)
            {
                if (connector != null)
                {
                    AddConnector(connector);
                }
            }

            _worker.DoWork += WorkerOnDoWork;
            _worker.WorkerSupportsCancellation = true;
            _worker.RunWorkerAsync();
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs args)
        {
            var me = (BackgroundWorker) sender;
            while (!me.CancellationPending)
            {
                Thread.Sleep(15000);
                UpdatePartyStates(null);
            }
        }

        public void UpdatePartyStates(IAgentInfo agentInfo)
        {
            Console.WriteLine("UpdatePartyStates");
            var msg = new UpdatePartyStatesMessage(_partyStates);

            Send(agentInfo, msg.ToString());
        }

        #region IServerMessageExecuter Members

        public void RegisterNewParty(IAgentInfo agent, string gameId, string parameters)
        {
            var status = RegisterNewPartyImpl(agent, gameId, parameters);
            var message = new AcknowledgeRegistrationMessage(status == PartyStatus.PartyRegistred, parameters).ToString();
            Send(agent, message);
            UpdatePartyStates(null);
        }

        private PartyStatus RegisterNewPartyImpl(IAgentInfo agent, string gameId, string parameters)
        {
            var player = _players.FirstOrDefault(pl => pl.Agent.Equals(agent));
            if (player == null)
            {
                throw new ArgumentException("Cannot find player of this agent");
            }

            if (_partyStates.FirstOrDefault(partyState => partyState.Players.FirstOrDefault(pl => pl.Equals(player)) != null) != null)
            {
                return PartyStatus.ClientDuplicated;
            }

            PartyState state = CreatePartyState(player, gameId, parameters);

            _partyStates.Add(state);

            return PartyStatus.PartyRegistred;
        }

        private PartyState CreatePartyState(IPlayer player, string gameId, string parameters)
        {
            return new PartyState
                       {
                           Status = PartyStatus.PartyRegistred,
                           GameTypeId = gameId,
                           Players = new[] {player},
                           Parameters = parameters,
                           PartyId = _partyStateId++,
                       };
        }

        public void RetrieveSupportedGames(IAgentInfo sender)
        {
            var array = _games.Select(info => info.Name + ',' + info.Id).ToArray();
            Console.WriteLine("number of games supported:" + array.Length);
            var message = new RetrieveSupportedGamesResponceMessage {Responce = array};
            Send(sender, message.ToString());
        }


        public void AcceptPartyRequest(int partyId, string accepterName)
        {
            var party = FindParty(partyId);

            AddPlayerToParty(party, FindPlayer(accepterName));

            // create the server component
            party.Server = _gameDict[party.GameTypeId].CreateServer(party.Players, party.Parameters);

            party.Server.OnMessage += OnServerOnOnMessage;

            // change the status
            party.Status = PartyStatus.Running;

            // notify observers
            UpdatePartyStates(null);

            //notify players
            var message = CreatePartyBeginMessage(party);
            foreach (var clientInfo in party.Players.Select(p=>p.Agent))
            {
                Send(clientInfo, message);
            }

            party.Server.Start();
        }

        private void OnServerOnOnMessage(object sender, OnMessageEventArgs args)
        {
            // need to send message to receipients
            var msg = new ClientGameMessage(args.Message).ToString();
            foreach (var clientInfo in args.Receipients.OfType<IPlayer>().Select(player => player.Agent))
            {
                Send(clientInfo, msg);
            }
        }

        private static string CreatePartyBeginMessage(PartyState party)
        {
            return new PartyBeginNotificationMessage(party.PartyId, party.GameTypeId, party.Parameters).ToString();
        }

        private static void AddPlayerToParty(PartyState party, IPlayer player)
        {
            party.Players = party.Players.Concat(Enumerable.Repeat(player, 1)).ToArray();
        }

        private IPlayer FindPlayer(string accepterName)
        {            
            if (accepterName == null) throw new ArgumentNullException();

            var player = _players.FirstOrDefault(p => p.PlayerName == accepterName);
            if (player == null) throw new ArgumentException();

            return player;
        }

        private PartyState FindParty(int partyId)
        {
            var party = _partyStates.FirstOrDefault(p => p.PartyId == partyId);
            if (party == null) throw new ArgumentException();
            return party;
        }

        #endregion

        private void ConnectorOnMessageArrived(object sender, MessageEventArgs args)
        {
            Console.WriteLine("ConnectorOnMessageArrived");
            string message = args.Message;
            var serializer = new XmlSerializer(typeof (ServerMessage), ServerMessageTypes.Types);
            var msgObject = (ServerMessage) serializer.Deserialize(new StringReader(message));
            Console.WriteLine(msgObject.GetType().ToString());
            msgObject.Execute(this, new AgentInfo { ClientConnector = (IMessageConnector)sender, ClientIdentifier = args.FromIdentifier });
        }

        public void AddConnector(IMessageConnector connector)
        {
            if (connector == null) throw new ArgumentNullException();

            connector.MessageArrived += ConnectorOnMessageArrived;
            connector.Closed += ConnectorOnClosed;
            connector.DisconnectedClient += ConnectorOnDisconnectedClient;
            _connectors.Add(connector);
        }

        private void ConnectorOnDisconnectedClient(object sender, DisconnectedClientEventArgs args)
        {
            var playersToRemove = _players.Where(player => player.Agent.ClientIdentifier.Equals(args.Identifier)).ToArray();
            foreach (var player in playersToRemove)
            {
                RemovePlayer(player);
            }
        }


        private void ConnectorOnClosed(object sender, EventArgs args)
        {
            Console.WriteLine("ConnectorOnClosed");
            var connector = (IMessageConnector)sender;

            foreach (var player in GetPlayersByConnection(connector))
            {
                RemovePlayer(player);
            }

            UpdatePartyStates(null);
        }

        private IPlayer[] GetPlayersByConnection(IMessageConnector connector)
        {
            var players = _players.Where(pl => pl.Agent.ClientConnector.Equals(connector)).ToArray();
            return players;
        }

        private void RemovePlayer(IPlayer playerInfo)
        {
            var parties = _partyStates.Where(state => state.Players.FirstOrDefault(player => player.PlayerName == playerInfo.PlayerName) != null).ToArray();
            foreach (var partyState in parties)
            {
                partyState.Dispose();
                _partyStates.Remove(partyState);
            }

            _players.Remove(playerInfo);
        }



        #region IServerMessageExecuter Members


        public void RegisterNewPlayer(IAgentInfo agent, string name)
        {
            if (agent == null)
            {
                throw new ArgumentNullException("agent");
            }

            bool status = false;
            if (_players.FirstOrDefault(pl => pl.PlayerName == name) == null)
            {
                var player = new Player { PlayerName = name, Agent = agent };
                _players.Add(player);
                status = true;
            }

            agent.ClientConnector.FollowClient(agent.ClientIdentifier);
            
            Send(agent, new AcceptNewPlayerMessage { Status = status }.ToString());
            if (status)
            {
                UpdatePartyStates(agent);
            }
        }

        public void UnregisterPlayer(IAgentInfo agent)
        {
            _players.RemoveAll(player => player.Agent.ClientIdentifier == agent.ClientIdentifier);
        }

        public void ExecuteServerGameMessage(IAgentInfo sender, string text, int id)
        {
            var server = _partyStates.First(state => state.PartyId == id).Server;
            var player1 = _players.First(player => player.Agent.ClientIdentifier.Equals(sender.ClientIdentifier));
            server.ProcessMessage(player1, text);
        }

        #endregion

        #region IServerMessageExecuter Members

        /// <summary>
        /// Send the message to the given recepient
        /// </summary>
        /// <param name="recepient">if null, then to all recepient</param>
        /// <param name="message"></param>
        private void Send(IAgentInfo recepient, string message)
        {
            if (recepient != null)
            {
                recepient.ClientConnector.Send(recepient.ClientConnector.ConnectorName, recepient.ClientIdentifier, message);
            }
            else
            {
                foreach (var client in _players.Select(p => p.Agent).ToArray())
                {
                    Send(client, message);
                }
            }
        }

        #endregion

        public void Dispose()
        {
            _worker.CancelAsync();
            _worker.Dispose();
            InvokeClosed(EventArgs.Empty);
        }

        public event EventHandler Closed;

        private void InvokeClosed(EventArgs e)
        {
            EventHandler handler = Closed;
            if (handler != null) handler(this, e);
        }
    }
}