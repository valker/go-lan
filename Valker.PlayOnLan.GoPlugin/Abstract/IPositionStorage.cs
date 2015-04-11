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

        int ExistParent(IPosition knownChild, IPosition possibleParent);
    }
}