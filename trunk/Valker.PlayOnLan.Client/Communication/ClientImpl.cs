﻿using System;
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
        /// <summary>
        /// Available connections (local, xmpp)
        /// </summary>
        private List<IMessageConnector> _connectors = new List<IMessageConnector>();

        /// <summary>
        /// Local server should work within each client
        /// </summary>
        private ServerImpl _localServer;

        public ClientImpl()
        {
            IMessageConnector clientConnector = this.CreateLocalServer();

            this._connectors.Add(clientConnector);
            this._connectors.Add(new XmppTransportImpl("Xmpp"));

            foreach (IMessageConnector connector in this._connectors)
            {
                connector.MessageArrived += this.ConnectorOnMessageArrived;
            }
        }

        private IMessageConnector CreateLocalServer()
        {
            var localTransport = new LocalTransport();
            IMessageConnector serverConnector = localTransport.CreateMessageConnector("server");
            IMessageConnector clientConnector = localTransport.CreateMessageConnector("Local");

            this._localServer = new ServerImpl(new[] {serverConnector});
            return clientConnector;
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

        public void RegisterNewParty(string name, GameInfo gameInfo)
        {
            this.SendMessage(new RegisterNewPartyMessage(name, gameInfo));
        }

        #region Overrides of IClientMessageExecuter

        public void UpdateSupportedGames(object sender, string[] games)
        {
            this.SupportedGamesChanged(this, new SupportedGamesChangedEventArgs(games, sender));
        }

        public void ShowMessage(string text)
        {
            this.MessageToShow(this, new MessageEventArgs(text));
        }

        public void UpdatePartyStates(PartyState[] partyStates, IMessageConnector sender)
        {
            this.PartyStatesChanged(this, new PartyStatesArgs(partyStates, sender));
        }

        public event EventHandler<SupportedGamesChangedEventArgs> SupportedGamesChanged = delegate { };

        public event EventHandler<MessageEventArgs> MessageToShow = delegate { };

        public event EventHandler<PartyStatesArgs> PartyStatesChanged = delegate { };

        #endregion

        public string GetGameName(string type)
        {
            // todo: fix me
            return "aaa";
        }
    }
}