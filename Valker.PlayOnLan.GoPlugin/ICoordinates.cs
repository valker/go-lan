using System.Collections.Generic;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// ќпредел€ет координаты клетки в пространстве игры
    /// </summary>
    public interface ICoordinates
    {
        int NumberOfDimensions { get; }
        int GetCoordinate(int dimension);
        IEnumerable<ICoordinates> Neighbours(IPosition position);
    }
}