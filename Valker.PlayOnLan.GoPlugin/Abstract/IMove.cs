using System;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IMove
    {
        Tuple<IPosition, IMoveInfo> Perform(IPosition position, IPlayerProvider playerProvider);
    }
}