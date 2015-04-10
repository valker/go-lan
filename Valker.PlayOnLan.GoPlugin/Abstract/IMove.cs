using System;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IMove
    {
        IMoveConsequences Perform(IPosition position, IPlayerProvider playerProvider);
    }
}