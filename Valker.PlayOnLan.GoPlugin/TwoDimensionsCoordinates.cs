using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    public class TwoDimensionsCoordinates : ICoordinates, IEquatable<TwoDimensionsCoordinates>
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
            if (obj.GetType() != GetType()) return false;
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
            IEnumerable<ICoordinates> neighbours = GetNeibours().ToArray();
            return neighbours.Where(position.Exist);
        }

        private IEnumerable<ICoordinates> GetNeibours()
        {
            yield return new TwoDimensionsCoordinates(_x - 1, _y);
            yield return new TwoDimensionsCoordinates(_x + 1, _y);
            yield return new TwoDimensionsCoordinates(_x, _y - 1);
            yield return new TwoDimensionsCoordinates(_x, _y + 1);
        }

        public int CompareTo(ICoordinates other)
        {
            var o = other as TwoDimensionsCoordinates;
            if (o == null) throw new ArgumentException("should be TwoDimensionsCoordinates", "other");
            Contract.EndContractBlock();
            var result = GetCoordinate(0).CompareTo(o.GetCoordinate(0));
            if (result == 0)
            {
                result = GetCoordinate(1).CompareTo(o.GetCoordinate(1));
            }

            return result;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0};{1}", _x, _y);
        }
    }
}