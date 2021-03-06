﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Client.Communication;
using Valker.PlayOnLan.PluginLoader;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Server.Messages;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Server.Messages.Server;
using Valker.PlayOnLan.Server2008.Messages.Server;

namespace Valker.PlayOnLan.Client2008.Communication
{
    /// <summary>
    /// Implements client functionality common to all UI, all transports and all games
    /// </summary>
    public class ClientImpl : IClientMessageExecuter, IDisposable
    {
        private readonly List<IAgentInfo> _servers = new List<IAgentInfo>();

        private IGameClient _client;

        private readonly IEnumerable<IGameType> _games = new List<IGameType>(Loader.Load(Environment.CurrentDirectory));

        private readonly IDictionary<string, IGameType> _gameDict = new Dictionary<string, IGameType>();

        /// <summary>
        /// ClientName of the player
        /// </summary>
        public string Name { get; }

        public ClientImpl(string name, IServerForm parent, IEnumerable<IAgentInfo> serverConnectors)
        {
            Name = name;
            Parent = parent;

            foreach (var game in _games)
            {
                _gameDict.Add(game.Id, game);
            }

            _servers.AddRange(serverConnectors);

            foreach (var connector in _servers.Select(info => info.ClientConnector))
            {
                connector.MessageArrived += ConnectorOnMessageArrived;
            }
        }

        public IForm Parent { get; set; }

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

        private void SendMessage(Message message)
        {
            var messageText = message.ToString();
            foreach (var agentInfo in _servers)
            {
                agentInfo.ClientConnector.Send(Name, agentInfo.ClientIdentifier, messageText);
            }
        }

        public void RegisterNewParty(GameInfo gameInfo, IMainForm parent)
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
            foreach (var agentInfo in _servers)
            {
                agentInfo.ClientConnector.Dispose();
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

        public void PartyBeginNotification(IMessageConnector sender, int partyId, string gameTypeId, string parameters, string[] players, string thisPlayer)
        {
            if (_client != null) return;

            var playerProvider = new ClientPlayerProvider(players, thisPlayer);

            _client = _gameDict[gameTypeId].CreateClient(Parent, playerProvider);

            _client.OnMessageReady +=
                (delegate(object o, MessageEventArgs args)
                     {
                         var message = new ServerGameMessage(args.Message).ToString();

                         sender.Send(Name, sender.ConnectorName, message);
                     });

            Parent.RunInUiThread(delegate
            {
                IPlayingForm playingForm = _client.CreatePlayingForm(parameters, Name, Parent.Gui);
                playingForm?.Show(Parent);
            });
        }

        private IPlayerProvider CreatePlayerProvider()
        {
            throw new NotImplementedException();
        }

        public void ExecuteGameMessage(IMessageConnector sender, string message)
        {
            _client.ExecuteMessage(message);
        }

        #endregion
    }

    public class ClientPlayerProvider : IPlayerProvider
    {
        private readonly IPlayer[] _players;
        private readonly Player _thisPlayer;

        public ClientPlayerProvider(string[] players, string thisPlayer)
        {
            _players = players.Select(s => new Player() {PlayerName = s}).Cast<IPlayer>().ToArray();
            _thisPlayer = new Player() {PlayerName = thisPlayer};
        }

        public IPlayer[] GetPlayers()
        {
            return _players;
        }

        public IPlayer GetFirstPlayer()
        {
            throw new NotImplementedException();
        }

        public IPlayer GetNextPlayer(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public IPlayer GetMe()
        {
            return _thisPlayer;
        }
    }
}