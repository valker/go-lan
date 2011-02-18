using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
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

        private List<PartyRequest> _requests = new List<PartyRequest>();

        private IEnumerable<IMessageConnector> _connectors;

        public ServerImpl(IEnumerable<IMessageConnector> connectors)
        {
            this._connectors = connectors;
            foreach (IMessageConnector connector in connectors)
            {
                connector.MessageArrived += this.ConnectorOnMessageArrived;
            }
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
            var request = new PartyRequest(name, game);
            if(_requests.FirstOrDefault(partyRequest => partyRequest.Name == name) != null)
            {
                return PartyStatus.NameDuplicated;
            }

            _requests.Add(request);
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