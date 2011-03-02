using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Server.Messages;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Server.Messages.Server;

namespace Valker.PlayOnLan.Client.Communication
{
    public class ClientImpl : IClientMessageExecuter, IDisposable
    {
        /// <summary>
        /// Available connections (local, xmpp)
        /// </summary>
        private List<IMessageConnector> _connectors = new List<IMessageConnector>();

        public ClientImpl(string name, IEnumerable<IMessageConnector> connectors)
        {
            Name = name;
            _connectors.AddRange(connectors);
            foreach (IMessageConnector connector in _connectors)
            {
                connector.MessageArrived += this.ConnectorOnMessageArrived;
            }
        }

        public string Name { get; set; }

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
                connector.Send(messageText);
            }
        }

        public void RegisterNewParty(GameInfo gameInfo)
        {
            this.SendMessage(new RegisterNewPartyMessage(Name, gameInfo));
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

        #region Implementation of IDisposable

        public void Dispose()
        {
            foreach (IMessageConnector connector in _connectors)
            {
                connector.Dispose();
            }
        }

        #endregion

        internal void AcceptParty(PartyInfo partyInfo)
        {
            SendMessage(new AcceptNewPartyMessage(partyInfo.Name, partyInfo.GameType, Name));
        }
    }
}