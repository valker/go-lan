using System;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{

    public interface IRule {}

    /// <summary>
    /// Описывает правила игры
    /// </summary>
    public interface IRules
    {
        Tuple<bool, ExceptionReason> IsMoveAcceptableInPosition(IMove move, IPosition position);

        Tuple<bool, ExceptionReason> IsPositionAcceptableInGameLine(IPosition oldPosition, IPosition position, IPositionStorage gameLine);

        double GetInitialScore(IPlayer player);
        void IsAcceptable(IMoveConsequences moveConsequences);
    }

    /// <summary>
    /// Определяет размер коми
    /// </summary>
    interface IKomiRule : IRule {
        double GetScore(int order);
    }

    /// <summary>
    /// Определяет как распределяются камни при форе
    /// </summary>
    internal interface IHandicapPositionRule : IRule
    {
    }

    /// <summary>
    /// определяет, как отображаются камни на цвета
    /// </summary>
    interface IColorMappingRule : IRule
    {
    }

    /// <summary>
    /// определяет, допустим ли ход по правилам ко
    /// </summary>
    interface IKoRule : IRule
    {
    }
}
