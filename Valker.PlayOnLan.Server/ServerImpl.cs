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

        private IEnumerable<IMessageConnector> _connectors;

        private BackgroundWorker _worker = new BackgroundWorker();

        public ServerImpl(IEnumerable<IMessageConnector> connectors)
        {
            this._connectors = connectors;
            foreach (IMessageConnector connector in connectors)
            {
                connector.MessageArrived += this.ConnectorOnMessageArrived;
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
                connector.SendMessage(message);
            }
        }

        public PartyStatus RegisterNewParty(string name, string gameId)
        {
            var game = _games.First(info => info.Id == gameId);
            if(this._partyStates.FirstOrDefault(partyState => partyState.Name == name) != null)
            {
                return PartyStatus.NameDuplicated;
            }

            var state = new PartyState();
            state.Name = name;
            state.Status = PartyStatus.PartyRegistred;
            state.GameId = gameId;

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
            msgObject.Execute(this);
        }
    }
}