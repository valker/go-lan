using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Server.Messages;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Server.Messages.Server;
using System.Windows.Forms;

namespace Valker.PlayOnLan.Client.Communication
{
    public class ClientImpl : IClientMessageExecuter, IDisposable
    {
        /// <summary>
        /// Available connections (local, xmpp)
        /// </summary>
        private List<IMessageConnector> _connectors = new List<IMessageConnector>();

        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name { get; set; }

        public ClientImpl(string name, IEnumerable<IMessageConnector> connectors)
        {
            Name = name;
            _connectors.AddRange(connectors);
            foreach (IMessageConnector connector in _connectors)
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

        private void SendMessage(Valker.PlayOnLan.Server.Messages.Message message)
        {
            string messageText = message.ToString();
            foreach (IMessageConnector connector in this._connectors)
            {
                connector.Send("_server", Name, messageText);
            }
        }

        public void RegisterNewParty(GameInfo gameInfo, Form parent)
        {
            this.SendMessage(new RegisterNewPartyMessage(gameInfo));
        }

        #region Overrides of IClientMessageExecuter

        public void UpdateSupportedGames(object sender, string[] games)
        {
            this.SupportedGamesChanged(this, new SupportedGamesChangedEventArgs(games, sender));
        }

        public void UpdatePartyStates(PartyState[] partyStates, IMessageConnector sender)
        {
            this.PartyStatesChanged(this, new PartyStatesArgs(partyStates, sender));
        }

        public event EventHandler<SupportedGamesChangedEventArgs> SupportedGamesChanged = delegate { };

        public event EventHandler<MessageEventArgs> MessageToShow = delegate { };

        public event EventHandler<PartyStatesArgs> PartyStatesChanged = delegate { };

        public event EventHandler<AcceptedPlayerEventArgs> AcceptedPlayer = delegate { };

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

        internal void RegisterNewPlayer()
        {
            SendMessage(new RegisterNewPlayerMessage() { Name = Name });
        }

        #region IClientMessageExecuter Members


        public void AcceptNewPlayer(bool status)
        {
            AcceptedPlayer(this, new AcceptedPlayerEventArgs() { Status = status });
        }

        #endregion
    }
}