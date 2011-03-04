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

        private List<IMessageConnector> _connectors = new List<IMessageConnector>();

        private List<IClientInfo> _clients = new List<IClientInfo>();

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

        public void UpdatePartyStates()
        {
            var msg = new UpdatePartyStatesMessage(this._partyStates);

            //this.Send(msg.ToString());
        }

        #region IServerMessageExecuter Members

        public PartyStatus RegisterNewParty(IClientInfo client, string playerName, string gameId)
        {
            if(this._partyStates.FirstOrDefault(partyState => partyState.players.FirstOrDefault(player => player.Name == playerName) != null) != null)
            {
                return PartyStatus.NameDuplicated;
            }

            if (_players.FirstOrDefault(player => player.Client.Equals(client)) != null)
            {
                return PartyStatus.ClientDuplicated;
            }

            var state = new PartyState {Status = PartyStatus.PartyRegistred, GameTypeId = gameId};
            state.players = new[] {new Player(){Client = client, Name = playerName}};
            state.playerNames = new[] {playerName};

            this._partyStates.Add(state);
            return PartyStatus.PartyRegistred;
        }

        public string[] RetrieveSupportedGames()
        {
            return _games.Select(info => info.Name + ',' + info.ID).ToArray();
        }


        public void AcceptPartyRequest(string RequesterName, string GameType, string AccepterName)
        {
            throw new NotImplementedException();
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

            foreach (var client in GetClientsByConnection(connector))
            {
                _clients.Remove(client);
            }

            UpdatePartyStates();
        }

        private IPlayer[] GetPlayersByConnection(IMessageConnector connector)
        {
            var players = _players.Where(pl => pl.Client.ClientConnector.Equals(connector)).ToArray();
            return players;
        }

        private IClientInfo[] GetClientsByConnection(IMessageConnector connector)
        {
            var clients = _clients.Where(cl => cl.ClientConnector.Equals(connector)).ToArray();
            return clients;
        }

        private void RemovePlayer(IPlayer playerInfo)
        {
            var parties = _partyStates.Where(state => state.players.FirstOrDefault(player => player.Name == playerInfo.Name) != null).ToArray();
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
            if (_players.FirstOrDefault(pl => pl.Name == Name) == null)
            {
                var player = new Player() { Name = Name, Client = client };
                _players.Add(player);
                status = true;
            }
            
            Send(client, new AcceptNewPlayerMessage() { Status = status }.ToString());
        }

        #endregion

        #region IServerMessageExecuter Members


        public void Send(IClientInfo recepient, string message)
        {
            recepient.ClientConnector.Send("_server", recepient.ClientIdentifier, message);
        }

        #endregion
    }
}