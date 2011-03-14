using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Server.Messages.Server;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;
using Valker.TicTacToePlugin;

namespace Valker.PlayOnLan.Client.Communication
{
    public class ClientImpl : IClientMessageExecuter, IDisposable
    {
        /// <summary>
        /// Available connections (local, xmpp)
        /// </summary>
        private List<IMessageConnector> _connectors = new List<IMessageConnector>();

        private IGameClient _client;

        private IEnumerable<IGameType> _games = new List<IGameType>(new IGameType[] {new TicTacToeGame()});

        private IDictionary<string, IGameType> _gameDict = new Dictionary<string, IGameType>();

        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name { get; set; }

        public ClientImpl(string name, Form parent, IEnumerable<IMessageConnector> connectors)
        {
            Name = name;
            Parent = parent;

            foreach (var game in _games)
            {
                _gameDict.Add(game.ID, game);
            }

            _connectors.AddRange(connectors);
            foreach (var connector in _connectors)
            {
                connector.MessageArrived += ConnectorOnMessageArrived;
            }
        }

        private Form Parent { get; set; }

        private void ConnectorOnMessageArrived(object sender, MessageEventArgs args)
        {
            var serializer = new XmlSerializer(typeof (ClientMessage), ClientMessageTypes.Types);
            var stringReader = new StringReader(args.Message);
            var msg = (ClientMessage) serializer.Deserialize(stringReader);
            msg.Execute(this, sender);
        }

        public void RetrieveSupportedGames()
        {
            SendMessage(new RetrieveSupportedGamesMessage());
        }

        private void SendMessage(Server.Messages.Message message)
        {
            var messageText = message.ToString();
            foreach (var connector in _connectors)
            {
                connector.Send("_server", Name, messageText);
            }
        }

        public void RegisterNewParty(GameInfo gameInfo, Form parent)
        {
            _client = _gameDict[gameInfo.GameId].CreateClient();
            var parameters = _client.AskParams(parent);
            SendMessage(new RegisterNewPartyMessage(gameInfo.GameId, parameters.ToString()));
        }

        #region Overrides of IClientMessageExecuter

        public void UpdateSupportedGames(object sender, string[] games)
        {
            SupportedGamesChanged(this, new SupportedGamesChangedEventArgs(games, sender));
        }

        public void UpdatePartyStates(PartyState[] partyStates, IMessageConnector sender)
        {
            PartyStatesChanged(this, new PartyStatesArgs(partyStates, sender));
        }

        public event EventHandler<SupportedGamesChangedEventArgs> SupportedGamesChanged = delegate { };

        public event EventHandler<PartyStatesArgs> PartyStatesChanged = delegate { };

        /// <summary>
        /// Raised when new player has accepted by server
        /// </summary>
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
            SendMessage(new AcceptNewPartyMessage(partyInfo.PartyId, Name));
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

        public void AcknowledgeRegistration(bool status, string parameters)
        {
            if (!status) return;
            var form = _client.CreatePlayingForm(parameters);
            form.Show(Parent);
        }

        public void PartyBeginNotification(int partyId, string gameTypeId, string parameters)
        {
            if (_client == null)
            {
                _client = _gameDict[gameTypeId].CreateClient();
                var form = _client.CreatePlayingForm(parameters);
                form.Show(Parent);
            }
        }

        #endregion
    }
}