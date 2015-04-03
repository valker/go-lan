using System;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public class PlayerProvider : IPlayerProvider
    {
        private readonly IPlayer[] _players;

        public PlayerProvider(IPlayer[] players)
        {
            _players = players;
        }

        public IPlayer[] GetPlayers()
        {
            return _players;
        }

        public IPlayer GetFirstPlayer()
        {
            return _players[0];
        }

        public IPlayer GetNextPlayer(IPlayer player)
        {
            var index = Array.FindIndex(_players, player1 => player1.Equals(player));
            ++index;
            if (index >= _players.Length)
            {
                index -= _players.Length;
            }
            return _players[index];
        }
    }
}