using System.Diagnostics.Contracts;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin.Abstract
{
    /// <summary>
    /// Правило, которое определяет нюансы игры. Все правила выполняются по цепочке, следующее
    /// правило в цепочке получает результат выполнения предыдущего правила
    /// </summary>
    [ContractClass(typeof(RuleContract))]
    public interface IRule
    {
        /// <summary>
        /// Определяет приемлем ли этот ход в данной позиции (правило КО)
        /// </summary>
        /// <param name="previousAcceptableMove"></param>
        /// <param name="oldPosition"></param>
        /// <param name="moveConsequences"></param>
        /// <param name="positionStorage"></param>
        /// <returns></returns>
        bool IsAcceptableMove(bool previousAcceptableMove, IPosition oldPosition, IMoveConsequences moveConsequences,
            IPositionStorage positionStorage);

        /// <summary>
        /// Определяет начальную позицию камней и очков
        /// </summary>
        /// <param name="previousHandicapPosition"></param>
        /// <param name="player"></param>
        /// <param name="players"></param>
        /// <returns></returns>
        IPosition GetInitialPosition(IPosition previousHandicapPosition, IPlayer player, IPlayer[] players);

        bool CheckFinish(bool previousFinish, IPosition currentPosition, IPositionStorage positionStorage);
    }

    [ContractClassFor(typeof(IRule))]
    public abstract class RuleContract : IRule
    {
        public bool IsAcceptableMove(bool previousAcceptableMove, IPosition oldPosition, IMoveConsequences moveConsequences,
            IPositionStorage positionStorage)
        {
            Contract.Requires(oldPosition != null);
            Contract.Requires(moveConsequences != null);
            Contract.Requires(positionStorage != null);
            return false;
        }

        public IPosition GetInitialPosition(IPosition previousHandicapPosition, IPlayer player, IPlayer[] players)
        {
            Contract.Requires(previousHandicapPosition != null);
            Contract.Requires(player != null);
            Contract.Requires(players != null);
            Contract.Requires(players.Length >= 2);
            return null;
        }

        public bool CheckFinish(bool previousFinish, IPosition currentPosition, IPositionStorage positionStorage)
        {
            Contract.Requires(currentPosition != null);
            Contract.Requires(positionStorage != null);
            return false;
        }
    }
}
