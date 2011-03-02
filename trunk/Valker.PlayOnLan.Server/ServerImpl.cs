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

        private List<PartyState> _partyStates = new List<PartyState>();

        //private List<IMessageConnector> _connectors = new List<IMessageConnector>();
        private List<ConnectionInfo> _connections = new List<ConnectionInfo>();

        private List<PlayerInfo> _players = new List<PlayerInfo>();
        
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

        public void UpdatePartyStates()
        {
            var msg = new UpdatePartyStatesMessage(this._partyStates);
            this.Send(msg.ToString());
        }

        #region IServerMessageExecuter Members

        public void Send(string message)
        {
            foreach (var connector in this._connections)
            {
                connector.Connector.Send(message);
            }
        }

        public PartyStatus RegisterNewParty(string name, string gameId, IMessageConnector connector)
        {
            if(this._partyStates.FirstOrDefault(partyState => partyState.players.FirstOrDefault(player => player.Name == name) != null) != null)
            {
                return PartyStatus.NameDuplicated;
            }

            var state = new PartyState {Status = PartyStatus.PartyRegistred, GameTypeId = gameId};
            state.players = new[] {new Player(){connector = connector, Name = name}};
            state.playerNames = new[] {name};

            this._partyStates.Add(state);
            return PartyStatus.PartyRegistred;
        }

        public string[] RetrieveSupportedGames()
        {
            return _games.Select(info => info.Name + ',' + info.ID).ToArray();
        }

        #endregion

        private void ConnectorOnMessageArrived(object sender, MessageEventArgs args)
        {
            string message = args.Message;
            var serializer = new XmlSerializer(typeof (ServerMessage), ServerMessageTypes.Types);
            var msgObject = (ServerMessage) serializer.Deserialize(new StringReader(message));
            msgObject.Execute(this, sender);
        }

        public bool AddConnector(IMessageConnector connector, string playerName)
        {
            if (_players.FirstOrDefault(pl => pl.Name == playerName) != null)
            {
                return false;
            }

            connector.MessageArrived += this.ConnectorOnMessageArrived;
            connector.Closed += ConnectorOnClosed;
            var connectionInfo = new ConnectionInfo() { Connector = connector };
            _connections.Add(connectionInfo);
            var playerInfo = new PlayerInfo();
            playerInfo.Connection = connectionInfo;
            playerInfo.Name = playerName;

            _players.Add(playerInfo);
            return true;
        }

        public void AddConnector(IMessageConnector connector)
        {
            connector.MessageArrived += this.ConnectorOnMessageArrived;
            connector.Closed += ConnectorOnClosed;
            _connections.Add(new ConnectionInfo() { Connector = connector });
        }


        private void ConnectorOnClosed(object sender, EventArgs args)
        {
            var connector = (IMessageConnector) sender;

            var connectionInfo = _connections.Find(ci => ci.Connector.Equals(connector));

            foreach (var player in GetPlayersByConnection(connectionInfo))
            {
                RemovePlayer(player);
            }

            _connections.Remove(connectionInfo);

            UpdatePartyStates();
        }

        private PlayerInfo[] GetPlayersByConnection(ConnectionInfo connectionInfo)
        {
            var playerToRemove = _players.Where(pl => pl.Connection.Equals(connectionInfo)).ToArray();
            return playerToRemove;
        }

        private void RemovePlayer(PlayerInfo playerInfo)
        {
            var parties = _partyStates.Where(state => state.players.FirstOrDefault(player => player.Name == playerInfo.Name) != null).ToArray();
            foreach (var partyState in parties)
            {
                partyState.Dispose();
                _partyStates.Remove(partyState);
            }

            _players.Remove(playerInfo);
        }

    }
}