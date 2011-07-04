using System;
using System.Linq;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Utilities;

namespace Valker.TicTacToePlugin
{
    /// <summary>
    /// Implements server part of Tic-Tac-Toe
    /// </summary>
    class TicTacToeServer : IGameServer
    {
        private int _currentPlayer;
        private Field _field;

        public TicTacToeServer(IPlayerBase[] players, string parameters)
        {
            if (players.Length != 2) throw new ArgumentException("Wrong number of players");
            Players = players;
            _currentPlayer = 0;
            Parameters = TicTacToeParameters.Parse(parameters);
        }

        protected TicTacToeParameters Parameters { get; set; }

        protected IPlayerBase[] Players { get; set; }

        readonly Stone[] _stones = new[] {Stone.Black, Stone.White, };

        public void ProcessMessage(IPlayer sender, string message)
        {
            switch (Util.ExtractCommand(message))
            {
                case "M":
                    MoveCommand(Util.ExtractParams(message).Select(s => Convert.ToInt32(s)).ToArray());
                    break;
                default:
                    throw new InvalidOperationException("wrong message " + message);
            }
        }

        private void MoveCommand(int[] coordinates)
        {
            ValidateCoordinates(coordinates);

            int x = coordinates[0];
            int y = coordinates[1];
            _field.Set(x, y, _stones[_currentPlayer]);

            var msg = string.Format("FC[{0},{1},{2}]", x, y, _stones[_currentPlayer]);
            SendMessageToPlayer(0, msg);
            SendMessageToPlayer(1, msg);

            var winner = _field.Win(Parameters.Stones);
            if (winner==Stone.None)
            {
                SwitchPlayers();
                AllowMove();
            } 
            else
            {
                msg = "WA";
                SendMessageToPlayer(0, msg);
                SendMessageToPlayer(1, msg);
                msg = string.Format("MSG[{0} has won]", winner == Stone.Black ? "Black" : "White");
                SendMessageToPlayer(0, msg);
                SendMessageToPlayer(1, msg);
            }
        }

        private void ValidateCoordinates(int[] coordinates)
        {
            foreach (var i in coordinates)
            {
                if (i >= Parameters.Width || i < 0)
                {
                    throw new InvalidOperationException("wrong coordinates");
                }
            }
        }

        private void SwitchPlayers()
        {
            _currentPlayer = 1 - _currentPlayer;
        }

        public event EventHandler<OnMessageEventArgs> OnMessage = delegate { };

        private void InvokeOnMessage(OnMessageEventArgs e)
        {
            EventHandler<OnMessageEventArgs> handler = OnMessage;
            if (handler != null) handler(this, e);
        }

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
            InvokeOnMessage(new OnMessageEventArgs(){Receipients = new []{Players[player]}, Message = message});
        }
    }
}
