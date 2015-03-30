using System;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Rules : IRules
    {
        public KoRule Ko { get; set; }
        public ScoreRule Score { get; set; }
        public bool IsAcceptable(Tuple<IPosition, IMoveInfo> newPosition, Pair<int, IPosition> distance)
        {
            //TODO: implement logic here
            return true;
        }
    }
}