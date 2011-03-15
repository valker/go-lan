using System;
using Valker.PlayOnLan.Api.Game;

namespace Valker.TicTacToePlugin
{
    class TicTacToeServer : IGameServer
    {
        public TicTacToeServer()
        {
        }

        public void ProcessMessage(IPlayer sender, string message)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<OnMessageEventArgs> OnMessage = delegate { };
    }
}
