using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Server.Messages.Server;
using Valker.TicTacToePlugin;

namespace Valker.PlayOnLan.Server
{
    public class ServerImpl : IServerMessageExecuter
    {
        // Games that are supported by this server
        private static readonly IGameServer[] _games = new IGameType[] 
        { 
            /*new MyGoban.GoRules(),*/ 
            new TicTacToePlugin.TicTacToeGame() 
        }.Select(plugin => plugin.CreateServer()).ToArray();

        // Added when new party registred
        // Removed when party is removed, OR client that register the party is removed
        private List<PartyState> _partyStates = new List<PartyState>();

        // Added when new transport is attached to the server
        private List<IMessageConnector> _connectors = new List<IMessageConnector>();

        // Added when new player is registred
        private List<IPlayer> _players = new List<IPlayer>();
        
        private BackgroundWorker _worker = new BackgroundWorker();

        public ServerImpl(IEnumerable<IMessageConnector> connectors)
        {
            foreach (IMessageConnector connector in connectors)
            {
                this.AddConnector(connector);
            }

            _worker.DoWork += WorkerOnDoWork;
            _worker.RunWorkerAsync();
        }

        private void WorkerOnDoWork(object sender, DoWorkEventArgs args)
        {
            while (true)
            {
                Thread.Sleep(15000);
                this.UpdatePartyStates();
            }
        }

        public void UpdatePartyStates(IClientInfo clientInfo = null)
        {
            var msg = new UpdatePartyStatesMessage(this._partyStates);

            Send(clientInfo, msg.ToString());
        }

        #region IServerMessageExecuter Members

        public PartyStatus RegisterNewParty(IClientInfo client, string gameId, string parameters)
        {
            var player = _players.FirstOrDefault(pl => pl.Client.Equals(client));
            if (player == null)
            {
                throw new ArgumentException("Cannot find player of this client");
            }

            if (this._partyStates.FirstOrDefault(partyState => partyState.Players.FirstOrDefault(pl => pl.Equals(player)) != null) != null)
            {
                return PartyStatus.ClientDuplicated;
            }

            var state = new PartyState {Status = PartyStatus.PartyRegistred, GameTypeId = gameId};
            state.Players = new[] {player};

            this._partyStates.Add(state);
            return PartyStatus.PartyRegistred;
        }

        public string[] RetrieveSupportedGames()
        {
            return _games.Select(info => info.Name + ',' + info.ID).ToArray();
        }


        public void AcceptPartyRequest(string RequesterName, string GameType, string AccepterName)
        {
            var party = _partyStates.First(g => g.Players.FirstOrDefault(p => p.PlayerName == RequesterName) != null);
            var players = new List<IPlayer>(party.Players);
            var player = _players.First(p => p.PlayerName == AccepterName);
            players.Add(player);
            party.Status = PartyStatus.Running;
            IGameServer server = new TicTacToeGame().CreateServer();
            party.Server = server;
            UpdatePartyStates();
        }

        #endregion

        private void ConnectorOnMessageArrived(object sender, MessageEventArgs args)
        {
            string message = args.Message;
            var serializer = new XmlSerializer(typeof (ServerMessage), ServerMessageTypes.Types);
            var msgObject = (ServerMessage) serializer.Deserialize(new StringReader(message));
            msgObject.Execute(this, new ClientInfo() { ClientConnector = (IMessageConnector)sender, ClientIdentifier = args.ToIdentifier});
        }

        public void AddConnector(IMessageConnector connector)
        {
            connector.MessageArrived += this.ConnectorOnMessageArrived;
            connector.Closed += ConnectorOnClosed;
            _connectors.Add(connector);
        }


        private void ConnectorOnClosed(object sender, EventArgs args)
        {
            var connector = (IMessageConnector) sender;

            foreach (var player in GetPlayersByConnection(connector))
            {
                RemovePlayer(player);
            }

            UpdatePartyStates();
        }

        private IPlayer[] GetPlayersByConnection(IMessageConnector connector)
        {
            var players = _players.Where(pl => pl.Client.ClientConnector.Equals(connector)).ToArray();
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


        public void RegisterNewPlayer(IClientInfo client, string Name)
        {
            bool status = false;
            if (_players.FirstOrDefault(pl => pl.PlayerName == Name) == null)
            {
                var player = new Player() { PlayerName = Name, Client = client };
                _players.Add(player);
                status = true;
            }
            
            Send(client, new AcceptNewPlayerMessage() { Status = status }.ToString());
            if (status)
            {
                UpdatePartyStates(client);
            }
        }

        #endregion

        #region IServerMessageExecuter Members

        /// <summary>
        /// Send the message to the given recepient
        /// </summary>
        /// <param name="recepient">if null, then to all recepient</param>
        /// <param name="message"></param>
        public void Send(IClientInfo recepient, string message)
        {
            if (recepient != null)
            {
                recepient.ClientConnector.Send("_server", recepient.ClientIdentifier, message);
            }
            else
            {
                foreach (var client in _players.Select(p => p.Client))
                {
                    Send(client, message);
                }
            }
        }

        #endregion
    }
}