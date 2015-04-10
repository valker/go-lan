using System;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public class ScoreChangedEventArgs : EventArgs
    {
        public ScoreChangedEventArgs(IPlayer player, double scoreDelta)
        {
            Player = player;
            ScoreDelta = scoreDelta;
        }

        public double ScoreDelta { get; set; }
        public IPlayer Player { get; set; }
    }
}