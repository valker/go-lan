using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Server.Messages.Server;

namespace Valker.PlayOnLan.Server
{
    public class ServerImpl : IServerMessageExecuter
    {
        private static readonly ServerGameInfo[] _games = new[]
                                                          {
                                                              new ServerGameInfo("Go", Guid.NewGuid()),
                                                              new ServerGameInfo("Atari go", Guid.NewGuid()),
                                                              new ServerGameInfo("Gomoku", Guid.NewGuid()),
                                                              new ServerGameInfo("Tic-tac-toe", Guid.NewGuid())
                                                          };

        private List<PartyState> _partyStates = new List<PartyState>();

        private List<IMessageConnector> _connectors = new List<IMessageConnector>();

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
            foreach (IMessageConnector connector in this._connectors)
            {
                connector.Send(message);
            }
        }

        public PartyStatus RegisterNewParty(string name, string gameId)
        {
            if(this._partyStates.FirstOrDefault(partyState => partyState.players.FirstOrDefault(player => player.Name == name) != null) != null)
            {
                return PartyStatus.NameDuplicated;
            }

            var state = new PartyState {Status = PartyStatus.PartyRegistred, GameTypeId = gameId};
            state.players = new[] {new Player(){connector = null, Name = name}};

            this._partyStates.Add(state);
            return PartyStatus.PartyRegistred;
        }

        public string[] RetrieveSupportedGames()
        {
            return _games.Select(info => info.ToString()).ToArray();
        }

        #endregion

        private void ConnectorOnMessageArrived(object sender, MessageEventArgs args)
        {
            string message = args.Message;
            var serializer = new XmlSerializer(typeof (ServerMessage), ServerMessageTypes.Types);
            var msgObject = (ServerMessage) serializer.Deserialize(new StringReader(message));
            msgObject.Execute(this, sender);
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
            _connectors.Remove(connector);
            _partyStates.RemoveAll(state => state.Name == connector.Name);
        }
    }

    public class Player : IPlayer
    {
        #region Implementation of IPlayer

        public IMessageConnector connector
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion
    }
}