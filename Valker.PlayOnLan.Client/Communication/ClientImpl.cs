using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Server.Messages;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Server.Messages.Server;
using Valker.PlayOnLan.XmppTransport;

namespace Valker.PlayOnLan.Client.Communication
{
    public class ClientImpl : IClientMessageExecuter
    {
        private List<IMessageConnector> _connectors = new List<IMessageConnector>();
        private ServerImpl _localServer;

        public ClientImpl()
        {
            var localTransport = new LocalTransport();
            IMessageConnector serverConnector = localTransport.CreateMessageConnector("server");
            IMessageConnector clientConnector = localTransport.CreateMessageConnector("Local");

            this._localServer = new ServerImpl(new[] {serverConnector});

            this._connectors.Add(clientConnector);
            this._connectors.Add(new XmppTransportImpl("Xmpp"));

            foreach (IMessageConnector connector in this._connectors)
            {
                connector.MessageArrived += this.ConnectorOnMessageArrived;
            }
        }

        private void ConnectorOnMessageArrived(object sender, MessageEventArgs args)
        {
            var serializer = new XmlSerializer(typeof (ClientMessage), ClientMessageTypes.Types);
            var stringReader = new StringReader(args.Message);
            var msg = (ClientMessage) serializer.Deserialize(stringReader);
            msg.Execute(this, sender);
        }

        public void RetrieveSupportedGames()
        {
            this.SendMessage(new RetrieveSupportedGamesMessage());
        }

        private void SendMessage(Message message)
        {
            string messageText = message.ToString();
            foreach (IMessageConnector connector in this._connectors)
            {
                connector.SendMessage(messageText);
            }
        }

        #region Overrides of IClientMessageExecuter

        public virtual void UpdateSupportedGames(object sender, string[] games)
        {
            this.SupportedGamesChanged(this, new SupportedGamesChangedEventArgs(games, sender));
        }

        public event EventHandler<SupportedGamesChangedEventArgs> SupportedGamesChanged = delegate { };

        #endregion
    }

    public class SupportedGamesChangedEventArgs : EventArgs
    {
        public SupportedGamesChangedEventArgs(string[] games, object sender)
        {
            this.Games = games;
            this.Sender = sender;
        }

        public object Sender { get; set; }

        public string[] Games { get; set; }
    }
}