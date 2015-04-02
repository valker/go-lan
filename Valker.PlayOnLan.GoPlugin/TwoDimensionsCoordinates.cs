using System;

namespace Valker.PlayOnLan.GoPlugin
{
    internal class TwoDimensionsCoordinates : ICoordinates
    {
        private readonly int _x;
        private readonly int _y;

        public TwoDimensionsCoordinates(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int NumberOfDimensions => 2;

        public int GetCoordinate(int dimension)
        {
            if (dimension == 0) return _x;
            if (dimension == 1) return _y;
            throw new ArgumentOutOfRangeException("dimension");
        }
    }
}