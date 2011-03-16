using System;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;

namespace Valker.TicTacToePlugin
{
    class TicTacToeServer : IGameServer
    {
        private int _currentPlayer;
        private Func<string, IMessage> _createGameMessage;

        public TicTacToeServer(IPlayer[] players)
        {
            if (players.Length != 2) throw new ArgumentException("Wrong number of players");
            Players = players;
            _currentPlayer = 0;
        }

        protected IPlayer[] Players { get; set; }

        public void ProcessMessage(IPlayer sender, string message)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<OnMessageEventArgs> OnMessage = delegate { };
        public void Start(Func<string, IMessage> createGameMessage)
        {
            _createGameMessage = createGameMessage;
            AllowMove();
        }

        private void AllowMove()
        {
            var client = Players[_currentPlayer].Client;

            client.ClientConnector.Send("", client.ClientIdentifier, _createGameMessage("AM").ToString());
        }
    }
}
