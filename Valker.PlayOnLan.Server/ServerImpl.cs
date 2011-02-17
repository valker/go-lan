using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Server;

namespace Valker.PlayOnLan.Server
{
    public class ServerImpl : IServerMessageExecuter
    {
        private static string[] _games = new[] {"Go", "Atari go", "Gomoku", "Tic-tac-toe"};
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

        public string[] RetrieveSupportedGames()
        {
            return _games;
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