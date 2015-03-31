using System;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IMove
    {
        Tuple<IPosition, IMoveInfo> Perform(IPosition position);
    }
}