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

        public double GetInitialScore(IPlayer player)
        {
            return _komiRule.GetScore(player.Order);
        }

        public void IsAcceptable(IPosition oldPosition, IMoveConsequences moveConsequences, IPositionStorage positionStorage)
        {
            var x = positionStorage.ExistParent(oldPosition, moveConsequences.Position);
            if(x > 0) throw new GoException(ExceptionReason.Ko);
        }
    }
}