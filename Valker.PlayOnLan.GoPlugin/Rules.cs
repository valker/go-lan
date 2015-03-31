using System;
using Valker.PlayOnLan.Api.Game;

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

        public Tuple<bool, ExceptionReason> IsMoveAcceptableInPosition(IMove move, IPosition position)
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, ExceptionReason> IsPositionAcceptableInGameLine(IPosition position, IPositionStorage gameLine)
        {
            throw new NotImplementedException();
        }

        public double GetInitialScore(IPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}