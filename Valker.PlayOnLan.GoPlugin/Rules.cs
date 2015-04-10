using System;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Rules : IRules
    {
        private IKomiRule _komiRule;
        public KoRule Ko { get; set; }
        public ScoreRule Score { get; set; }

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
            return _komiRule.GetScore(player.Order);
        }

        public void IsAcceptable(IMoveConsequences moveConsequences)
        {
            throw new NotImplementedException();
        }
    }
}