using System;
using System.Collections.Generic;
using System.IO;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.PluginLoader;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Server.Messages.Server;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Server2008.Messages.Server;

namespace Valker.PlayOnLan.Client.Communication
{
    public class ClientImpl : IClientMessageExecuter, IDisposable
    {
        /// <summary>
        /// Available connections (local, xmpp)
        /// </summary>
        private List<IMessageConnector> _connectors = new List<IMessageConnector>();

        private IGameClient _client;

        private IEnumerable<IGameType> _games = new List<IGameType>(Loader.Load(Environment.CurrentDirectory));

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
            var serializer = ClientMessageTypes.Serializer;
            var stringReader = new StringReader(args.Message);
            var msg = (ClientMessage)serializer.Deserialize(stringReader);
            msg.Execute(this, (IMessageConnector)sender);
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
                connector.Send(Name, "server@mosdb9vf4j", messageText);
            }
        }

        public void RegisterNewParty(GameInfo gameInfo, Form parent)
        {
            // ask user to set up parameters of the game
            var parameters = _gameDict[gameInfo.GameTypeId].AskParam(parent);

            // send registration message to the server
            SendMessage(new RegisterNewPartyMessage(gameInfo.GameTypeId, parameters));
        }

        #region Overrides of IClientMessageExecuter

        public void UpdateSupportedGames(IMessageConnector sender, string[] games)
        {
            SupportedGamesChanged(this, new SupportedGamesChangedEventArgs(games, sender));
        }

        public void UpdatePartyStates(IMessageConnector sender, PartyState[] partyStates)
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

        public void PartyBeginNotification(IMessageConnector sender, int partyId, string gameTypeId, string parameters)
        {
            if (_client != null) return;

            _client = _gameDict[gameTypeId].CreateClient(Parent);

            _client.OnMessageReady +=
                (delegate(object o, MessageEventArgs args)
                {
                    var message = new ServerGameMessage(args.Message).ToString();
                    sender.Send(Name, args.ToIdentifier, message);
                });

            var form = _client.CreatePlayingForm(parameters, Name);
            form.Show(Parent);
        }

        public void ExecuteGameMessage(IMessageConnector sender, string message)
        {
            _client.ExecuteMessage(message);
        }

        #endregion
    }
}