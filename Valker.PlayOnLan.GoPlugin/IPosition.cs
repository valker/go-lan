using System.Collections.Generic;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IPosition
    {
        Pair<IPosition, int> Move(Point point, Stone player);
        IEnumerable<Pair<Point, Stone>> CompareStoneField(IPosition next);
        Stone GetStoneAt(Point point);
        int Size { get; }
        bool IsEditable { get; }
    }
}
