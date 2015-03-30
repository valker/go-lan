using System;
using System.Collections.Generic;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IPosition
    {
        Tuple<IPosition, IMoveInfo> Move(Point point, Stone player);
        IEnumerable<Tuple<Point, Stone>> CompareStoneField(IPosition next);
        Stone GetStoneAt(Point point);
        int Size { get; }
        bool IsEditable { get; }
    }
}
