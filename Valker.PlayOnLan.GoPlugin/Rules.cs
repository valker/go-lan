using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Rules : IRules
    {
        private IKomiRule _komiRule;
        public KoRule Ko { get; set; }
        public ScoreRule Score { get; set; }

        public double GetInitialScore(IPlayer player)
        {
            return _komiRule.GetScore(player.Order);
        }

        public void IsAcceptable(IPosition oldPosition, IMoveConsequences moveConsequences, IPositionStorage positionStorage)
        {
            int x = positionStorage.ExistParent(oldPosition, moveConsequences.Position);
            if(x > 0) throw new GoException(ExceptionReason.Ko);
        }
    }
}