using System.Collections.Generic;
using System.Linq;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Rules : IRules
    {
        private readonly List<IRule> _rules = new List<IRule>();

        public bool IsAcceptableMove(bool previousAcceptableMove, IPosition oldPosition, IMoveConsequences moveConsequences,
            IPositionStorage positionStorage)
        {
            return _rules.Aggregate(
                previousAcceptableMove,
                (current, rule) => rule.IsAcceptableMove(current, oldPosition, moveConsequences, positionStorage));
        }

        public IPosition GetInitialPosition(IPosition previousHandicapPosition, IPlayer player, IPlayer[] players)
        {
            return _rules.Aggregate(
                previousHandicapPosition,
                (position, rule) => rule.GetInitialPosition(position, player, players));
        }

        public bool CheckFinish(bool previousFinish, IPosition currentPosition, IPositionStorage positionStorage)
        {
            return _rules.Aggregate(
                previousFinish,
                (b, rule) => rule.CheckFinish(b, currentPosition, positionStorage));
        }

        public void Add(IRule item)
        {
            _rules.Add(item);
        }
    }
}