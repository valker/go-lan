using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.Api
{
    /// <summary>
    /// Define an interface of game tree storage
    /// </summary>
    public interface IGameTree
    {
        void RegisterRootPosition(IPosition rootPosition);
        void RegisterPosition(IPosition previousPosition, IPosition currentPosition, IMoveInfo moveInfo);
        IPosition GetParentPosition(IPosition position);
        IEnumerable<IPosition> GetChildren(IPosition position);
    }
}
