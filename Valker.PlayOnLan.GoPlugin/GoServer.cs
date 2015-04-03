using System;
using System.Collections.Generic;
using System.Linq;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// Implements server part of the game: message translation, rules, etc..
    /// </summary>
    class GoServer : IGameServer
    {
        public GoServer(IPlayer[] players, string parameters)
        {
            if (players.Length != 2) throw new ArgumentException("Wrong number of players");
            Players = players;

            Parameters = Parameters.Parse(parameters);

            IPlayerProvider playerProvider = new PlayerProvider(players);
            Engine = new Engine(new PositionStorage(Parameters.Width, playerProvider), playerProvider, new Rules());
            Engine.CellChanged += EngineOnCellChanged;
            Engine.ScoreChanged += EngineOnEatedChanged;

//            _colors = new Stone[2];
//            _colors[0] = Stone.Black;
//            _colors[1] = Stone.White;
        }

        protected Parameters Parameters { get; set; }

        protected IPlayer[] Players { get; set; }

        protected IEngine Engine { get; set; }

        //private Stone[] _colors; 

        public void ProcessMessage(IPlayer sender, string message)
        {
            switch (Util.ExtractCommand(message))
            {
                case "MOVE":
                    Move(Util.ExtractParams(message));
                    break;
                case "PASS":
                    Pass();
                    break;
                case "RESIGN":
                    Resign(sender);
                    break;
                case "SEND":
                    Send(Util.ExtractParams(message));
                    break;
                case "DEAD":
                    Dead(Util.ExtractCommand(message));
                    break;
                case "LIVE":
                    Live(Util.ExtractParams(message));
                    break;
                case "AGREEMENT":
                    Agreement(sender);
                    break;
                case "DISAGREEMENT":
                    Disagreement(sender);
                    break;
                case "ASK":
                    Ask(sender, Util.ExtractParams(message));
                    break;
                default:
                    throw new InvalidOperationException("wrong message " + message);
            }
        }

        private void Ask(IPlayer player, string[] strings)
        {
            throw new NotImplementedException();
        }

        private void Disagreement(IPlayer player)
        {
            throw new NotImplementedException();
        }

        private void Agreement(IPlayer player)
        {
            throw new NotImplementedException();
        }

        private void Live(string[] strings)
        {
            throw new NotImplementedException();
        }

        private void Dead(string command)
        {
            throw new NotImplementedException();
        }

        private void Send(string[] strings)
        {
            throw new NotImplementedException();
        }

        private void Resign(IPlayer player)
        {
            throw new NotImplementedException();
        }

        private void Pass()
        {
            Engine.Move(new Pass());
            AllowMove();
        }

        private void Move(IEnumerable<string> strings)
        {
            int[] coordinates = strings.Take(2).Select(int.Parse).ToArray();
            Engine.Move(new Move(coordinates[0], coordinates[1]));
            AllowMove();
        }

        /// <summary>
        /// Raised when server part wants to send a message to client(s)
        /// </summary>
        public event EventHandler<OnMessageEventArgs> OnMessageReady;

        private void InvokeOnMessage(OnMessageEventArgs e)
        {
            EventHandler<OnMessageEventArgs> handler = OnMessageReady;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void Start()
        {
            SendInitialState();
            EngineOnEatedChanged(null, null);
            AllowMove();
        }

        private void EngineOnEatedChanged(object sender, EventArgs args)
        {
            var score = Engine.PlayerProvider.GetPlayers()
                .OrderBy(player => player.PlayerName)
                .Select(player => Engine.GetScore(player).ToString());
            var joined = string.Join(",", score);
            var message = string.Format("EATED[{0}]", joined);
            SendMessageToAllPlayers(message);
        }

        private void EngineOnCellChanged(object sender, CellChangedEventArgs args)
        {
            string message = string.Format("FIELD[{0},{1}]", args.Coordinates.ToString(), args.Cell.ToString());
            SendMessageToAllPlayers(message);
        }

        private void SendMessageToAllPlayers(string message)
        {
            foreach (var player in Players)
            {
                SendMessageToPlayer(player, message);
            }
        }

        private void SendInitialState()
        {
            string message = "PARAMS[width=" + Parameters.Width + "]";
            SendMessageToAllPlayers(message);
        }

        /// <summary>
        /// Sends to players who should move and who should wait
        /// </summary>
        private void AllowMove()
        {
            IPlayer currentPlayer = Engine.CurrentPlayer;

            foreach (var player in Engine.PlayerProvider.GetPlayers())
            {
                if (!player.Equals(currentPlayer))
                {
                    SendMessageToPlayer(player, "WAIT");
                }
            }

            SendMessageToPlayer(currentPlayer, "ALLOW");
        }

        private void SendMessageToPlayer(IPlayer player, string message)
        {
            InvokeOnMessage(new OnMessageEventArgs() { Receipients = new[] { player }, Message = message });
        }
    }
}
