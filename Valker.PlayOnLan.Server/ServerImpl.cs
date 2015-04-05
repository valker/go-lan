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
        // Added when new transport is attached to the server
        private readonly List<IMessageConnector> _connectors;
        private readonly IDictionary<string, IGameType> _gameDict;
        private readonly IEnumerable<IGameType> _games;
        // Added when new party registred
        // Removed when party is removed, OR client that register the party is removed
        private readonly List<PartyState> _partyStates;
        // Added when new player is registred
        private readonly List<IPlayer> _players;
        private readonly BackgroundWorker _worker;
        private int _partyStateId;

        public ServerImpl(IEnumerable<IMessageConnector> connectors)
        {
            if (connectors == null) throw new ArgumentNullException("connectors");

            _games = new List<IGameType>(Loader.Load(Environment.CurrentDirectory));

            _gameDict = new Dictionary<string, IGameType>();

            _partyStates = new List<PartyState>();

            _connectors = new List<IMessageConnector>();

            _players = new List<IPlayer>();

            _worker = new BackgroundWorker();

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

        public void Dispose()
        {
            _worker.CancelAsync();
            _worker.Dispose();
            InvokeClosed(EventArgs.Empty);
        }

        public void UpdatePartyStates(IAgentInfo agentInfo)
        {
            Console.WriteLine("UpdatePartyStates");
            var msg = new UpdatePartyStatesMessage(_partyStates);

            Send(agentInfo, msg.ToString());
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

        private void ConnectorOnMessageArrived(object sender, MessageEventArgs args)
        {
            Console.WriteLine("ConnectorOnMessageArrived");
            var message = args.Message;
            var serializer = new XmlSerializer(typeof (ServerMessage), ServerMessageTypes.Types);
            var msgObject = (ServerMessage) serializer.Deserialize(new StringReader(message));
            Console.WriteLine(msgObject.GetType().ToString());
            msgObject.Execute(this,
                new AgentInfo {ClientConnector = (IMessageConnector) sender, ClientIdentifier = args.FromIdentifier});
        }

        public void AddConnector(IMessageConnector connector)
        {
            if (connector == null) throw new ArgumentNullException();

            connector.MessageArrived += ConnectorOnMessageArrived;
            connector.Closed += ConnectorOnClosed;
            connector.DisconnectedOther += ConnectorOnDisconnectedClient;
            _connectors.Add(connector);
        }

        private void ConnectorOnDisconnectedClient(object sender, DisconnectedClientEventArgs args)
        {
            var playersToRemove =
                _players.Where(player => player.Agent.ClientIdentifier.Equals(args.Identifier)).ToArray();
            foreach (var player in playersToRemove)
            {
                RemovePlayer(player);
            }
        }

        private void ConnectorOnClosed(object sender, EventArgs args)
        {
            Console.WriteLine("ConnectorOnClosed");
            var connector = (IMessageConnector) sender;

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
            var parties =
                _partyStates.Where(
                    state => state.Players.FirstOrDefault(player => player.PlayerName == playerInfo.PlayerName) != null)
                    .ToArray();
            foreach (var partyState in parties)
            {
                partyState.Dispose();
                _partyStates.Remove(partyState);
            }

            _players.Remove(playerInfo);
        }

        #region IServerMessageExecuter Members

        /// <summary>
        ///     Send the message to the given recepient
        /// </summary>
        /// <param name="recepient">if null, then to all recepient</param>
        /// <param name="message"></param>
        private void Send(IAgentInfo recepient, string message)
        {
            if (recepient != null)
            {
                recepient.ClientConnector.Send(recepient.ClientConnector.ConnectorName, recepient.ClientIdentifier,
                    message);
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

        public event EventHandler Closed;

        private void InvokeClosed(EventArgs e)
        {
            var handler = Closed;
            if (handler != null) handler(this, e);
        }

        #region IServerMessageExecuter Members

        public void RegisterNewParty(IAgentInfo agent, string gameId, string parameters)
        {
            var status = RegisterNewPartyImpl(agent, gameId, parameters);
            var message =
                new AcknowledgeRegistrationMessage(status == PartyStatus.PartyRegistred, parameters).ToString();
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

            if (
                _partyStates.FirstOrDefault(
                    partyState => partyState.Players.FirstOrDefault(pl => pl.Equals(player)) != null) != null)
            {
                return PartyStatus.ClientDuplicated;
            }

            var state = CreatePartyState(player, gameId, parameters);

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
                PartyId = _partyStateId++
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

            party.Server.OnMessageReady += OnServerOnOnMessage;

            // change the status
            party.Status = PartyStatus.Running;

            // notify observers
            UpdatePartyStates(null);

            //notify players
            foreach (var player in party.Players)
            {
                var message = CreatePartyBeginMessage(party, player);
                Send(player.Agent, message);
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

        private static string CreatePartyBeginMessage(PartyState party, IPlayer player)
        {
            return
                new PartyBeginNotificationMessage(party.PartyId, party.GameTypeId, party.Parameters, party.Players.Select(player1 => player1.PlayerName).ToArray(), player.PlayerName)
                    .ToString();
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

        #region IServerMessageExecuter Members

        public void RegisterNewPlayer(IAgentInfo agent, string name)
        {
            if (agent == null)
            {
                throw new ArgumentNullException("agent");
            }

            var status = false;
            if (_players.FirstOrDefault(pl => pl.PlayerName == name) == null)
            {
                var player = new Player {PlayerName = name, Agent = agent, Order = _players.Count};
                _players.Add(player);
                status = true;
            }

            agent.ClientConnector.WatchOther(agent.ClientIdentifier);

            Send(agent, new AcceptNewPlayerMessage {Status = status}.ToString());
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
            var partyState = _partyStates.First(state => state.PartyId == id);
            var server = partyState.Server;
            var player1 = _players.First(player => player.Agent.ClientIdentifier.Equals(sender.ClientIdentifier));
            server.ProcessMessage(player1, text);
        }

        #endregion
    }

    internal class ServerPlayerProvider : IPlayerProvider
    {
        private IPlayer[] _players;
        private IPlayer _player;

        public ServerPlayerProvider(IPlayer[] players, IPlayer player)
        {
            _players = players;
            _player = player;
        }

        public IPlayer[] GetPlayers()
        {
            return _players;
        }

        public IPlayer GetFirstPlayer()
        {
            throw new NotImplementedException();
        }

        public IPlayer GetNextPlayer(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public IPlayer GetMe()
        {
            return _player;
        }
    }
}