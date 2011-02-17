using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages;

namespace Valker.PlayOnLan.Server
{
    public class ServerImpl : ServerMessageExecuter
    {
        private IEnumerable<IMessageConnector> _connectors;
        private static string[] _games = new[] {"Go", "Atari go", "Gomoku", "Tic-tac-toe"};

        public ServerImpl(IEnumerable<IMessageConnector> connectors)
        {
            _connectors = connectors;
            foreach (var connector in connectors)
            {
                connector.MessageArrived += ConnectorOnMessageArrived;
            }
        }

        public override void Send(string message)
        {
            foreach (var connector in _connectors)
            {
                connector.SendMessage(message);
            }
        }

        private void ConnectorOnMessageArrived(object sender, MessageEventArgs args)
        {
            var message = args.Message;
            XmlSerializer serializer = new XmlSerializer(typeof(Message), new [] {typeof(RetrieveSupportedGamesMessage)});
            Message msgObject = (Message) serializer.Deserialize(new StringReader(message));
            msgObject.Execute(this);
        }

        public override string[] RetrieveSupportedGames()
        {
            return _games;
        }
    }
}
