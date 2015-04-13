using System;
using System.Collections.Generic;

namespace Valker.PlayOnLan.GoPlugin.Abstract
{
    /// <summary>
    /// ќпредел€ет координаты клетки в пространстве игры
    /// </summary>
    public interface ICoordinates : IComparable<ICoordinates>
    {
        int NumberOfDimensions { get; }
        int GetCoordinate(int dimension);
        IEnumerable<ICoordinates> Neighbours(IPosition position);
    }
}