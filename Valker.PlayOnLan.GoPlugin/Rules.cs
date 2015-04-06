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
            return Tuple.Create(true, ExceptionReason.None);
        }

        public Tuple<bool, ExceptionReason> IsPositionAcceptableInGameLine(IPosition oldPosition, IPosition position, IPositionStorage gameLine)
        {
            var value = gameLine.ExistParent(oldPosition, position);
            if (value)
            {
                return Tuple.Create(false, ExceptionReason.Ko);
            }

            return Tuple.Create(true, ExceptionReason.None);
        }

        public double GetInitialScore(IPlayer player)
        {
            if (player.Order == 0) return 0.0;
            return 6.5;
        }
    }
}