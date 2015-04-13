using System;
using System.Collections.Generic;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// ���������� ���������� ������ � ������������ ����
    /// </summary>
    public interface ICoordinates : IComparable<ICoordinates>
    {
        int NumberOfDimensions { get; }
        int GetCoordinate(int dimension);
        IEnumerable<ICoordinates> Neighbours(IPosition position);
    }
}