using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Valker.PlayOnLan.GoPlugin
{
    internal class TwoDimensionsCoordinates : ICoordinates, IEquatable<TwoDimensionsCoordinates>
    {
        public bool Equals(TwoDimensionsCoordinates other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _x == other._x && _y == other._y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TwoDimensionsCoordinates) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_x*397) ^ _y;
            }
        }

        public static bool operator ==(TwoDimensionsCoordinates left, TwoDimensionsCoordinates right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TwoDimensionsCoordinates left, TwoDimensionsCoordinates right)
        {
            return !Equals(left, right);
        }

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

        public IEnumerable<ICoordinates> Neighbours(IPosition position)
        {
            IEnumerable<ICoordinates> neighbours = GetNeibours();
            return neighbours.Where(c => position.Exist(c));
        }

        private IEnumerable<ICoordinates> GetNeibours()
        {
            yield return new TwoDimensionsCoordinates(_x - 1, _y);
            yield return new TwoDimensionsCoordinates(_x + 1, _y);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0};{1}", _x, _y);
        }
    }
}