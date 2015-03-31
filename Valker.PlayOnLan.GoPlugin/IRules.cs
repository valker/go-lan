using System;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// Описывает правила игры
    /// </summary>
    public interface IRules
    {
        Tuple<bool, ExceptionReason> IsMoveAcceptableInPosition(IMove move, IPosition position);

        Tuple<bool, ExceptionReason> IsPositionAcceptableInGameLine(IPosition position, IPositionStorage gameLine);

        double GetInitialScore(IPlayer player);
    }
}
