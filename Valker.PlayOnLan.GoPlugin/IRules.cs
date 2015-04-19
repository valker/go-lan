using System.Diagnostics.Contracts;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    [ContractClass(typeof(RulesContract))]
    public interface IRules : IRule
    {
        void Add(IRule item);
    }

    [ContractClassFor(typeof(IRules))]
    public abstract class RulesContract : IRules
    {
        public void Add(IRule item)
        {
            Contract.Requires(item != null);
        }

        public abstract bool IsAcceptableMove(bool previousAcceptableMove, IPosition oldPosition, IMoveConsequences moveConsequences,
            IPositionStorage positionStorage);

        public abstract IPosition GetInitialPosition(IPosition previousHandicapPosition, IPlayer player, IPlayer[] players);
        public abstract bool CheckFinish(bool previousFinish, IPosition currentPosition, IPositionStorage positionStorage);
    }
}