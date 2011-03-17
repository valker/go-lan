using System;
using System.Linq;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;

namespace Valker.TicTacToePlugin
{
    class TicTacToeServer : IGameServer
    {
        private int _currentPlayer;
        private readonly Func<string, IMessage> _createGameMessage;
        private Field _field;

        public TicTacToeServer(IPlayer[] players, Func<string, IMessage> func, string parameters)
        {
            if (players.Length != 2) throw new ArgumentException("Wrong number of players");
            Players = players;
            _currentPlayer = 0;
            _createGameMessage = func;
            Parameters = TicTacToeParameters.Parse(parameters);
        }

        protected TicTacToeParameters Parameters { get; set; }

        protected IPlayer[] Players { get; set; }

        Stone[] _stones = new Stone[] {Stone.Black, Stone.White, };

        public void ProcessMessage(IPlayer sender, string message)
        {
            switch (MessageUtils.ExtractCommand(message))
            {
                case "M":
                    var param = MessageUtils.ExtractParams(message).Select(s => Convert.ToInt32(s)).ToArray();
                    foreach (var i in param)
                    {
                        if (i >= Parameters.Width || i < 0)
                        {
                            throw new InvalidOperationException("wrong coordinates");
                        }
                    }

                    int x = param[0];
                    int y = param[1];
                    _field.Set(x, y, _stones[_currentPlayer]);

                    var msg = string.Format("FC<{0},{1},{2}>", x, y, _stones[_currentPlayer]);
                    SendMessageToPlayer(0, msg);
                    SendMessageToPlayer(1, msg);

                    if (Win(x,y)==Stone.None)
                    {

                        _currentPlayer = 1 - _currentPlayer;
                        AllowMove();
                    }
                    break;

            }
        }

        private Stone Win(int x, int y)
        {
            int yy;
            for(yy = 0; yy < Parameters.Width; ++yy)
            {
                var b = CheckHorizont(yy);
                if (b != Stone.None)
                {
                    return b;
                }
            }
            return Stone.None;
        }

        /// <summary>
        /// Check that so
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private Stone CheckHorizont(int y)
        {
            Stone last = Stone.None;
            int count = 0;
            for(int x = 0; x < Parameters.Width; ++x)
            {
                if (_field.Get(x, y) == last && last != Stone.None)
                {
                    ++count;
                }
                else if (count >= Parameters.Stones)
                {
                    return last;
                }
            }
            return Stone.None;
        }

        public event EventHandler<OnMessageEventArgs> OnMessage = delegate { };
        public void Start()
        {
            _field = new Field(Parameters.Width);
            AllowMove();
        }

        private void AllowMove()
        {
            SendMessageToPlayer(_currentPlayer, "AM");
            SendMessageToPlayer(1 - _currentPlayer, "WA");
        }

        private void SendMessageToPlayer(int player, string message)
        {
            var client = Players[player].Client;
            client.ClientConnector.Send("", client.ClientIdentifier, _createGameMessage(message).ToString());
        }
    }
}
