using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    class GoServer : IGameServer
    {
        private int _currentPlayer;

        public GoServer(IPlayer[] players, string parameters)
        {
            if (players.Length != 2) throw new ArgumentException("Wrong number of players");
            Players = players;
            _currentPlayer = 0;
            Parameters = Parameters.Parse(parameters);

        }

        protected Parameters Parameters { get; set; }

        protected IPlayer[] Players { get; set; }

        protected Field Field { get; set; }

        public void ProcessMessage(IPlayer sender, string message)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<OnMessageEventArgs> OnMessage;

        private void InvokeOnMessage(OnMessageEventArgs e)
        {
            EventHandler<OnMessageEventArgs> handler = OnMessage;
            if (handler != null) handler(this, e);
        }

        public void Start()
        {
            Field = new Field(Parameters.Width);
            // todo: implement start of party
        }
    }
}
