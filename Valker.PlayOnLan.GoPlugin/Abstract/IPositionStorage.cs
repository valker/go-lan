using System;
using System.Collections.Generic;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IPositionStorage
    {
        /// <summary>
        /// ¬озвращает исходную позицию в дереве игры
        /// </summary>
        IPosition Initial { get; }

        void AddChildPosition(IPosition parent, IPosition child);

        bool ExistParent(IPosition knownChild, IPosition possibleParent);
//        /// <summary>
//        /// ¬озвращает позиции, которые встречались при анализе ситуации
//        /// после данной позиции
//        /// </summary>
//        /// <param name="position"></param>
//        /// <returns></returns>
//        IEnumerable<IPosition> GetChildPositions(IPosition position);
//        /// <summary>
//        /// ¬ополнить ход
//        /// </summary>
//        /// <param name="position">исходна€ позици€</param>
//        /// <param name="point">позици€ камн€</param>
//        /// <param name="player">цвет камн€ игрока</param>
//        /// <returns></returns>
//        Tuple<IPosition, IMoveInfo> Move(IPosition position, Point point, Stone player);
    }
}