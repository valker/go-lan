using System.Collections.Generic;
using System.Linq;

namespace Valker.PlayOnLan.GoPlugin
{
    public class CellField
    {
        private readonly Dictionary<ICoordinates, ICell> _field; 

        public CellField(CellField parent)
        {
            _field = new Dictionary<ICoordinates, ICell>(parent._field);
            Size = parent.Size;
        }

        public CellField(int size)
        {
            Size = size;
            _field = new Dictionary<ICoordinates, ICell>();
        }

        public int Size { get; }

        public void SetAt(ICoordinates coordinates, ICell cell)
        {
            _field[coordinates] = cell;
        }

        public ICell GetAt(ICoordinates coordinates)
        {
            ICell value;
            if (!_field.TryGetValue(coordinates, out value))
            {
                return new EmptyCell();
            }
            return value;
        }

        public bool Equals(CellField other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (_field.Keys.Count != other._field.Keys.Count) return false;
            return _field.All(keyValuePair => other.GetAt(keyValuePair.Key).Equals(keyValuePair.Value));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (CellField)) return false;
            return Equals((CellField) obj);
        }

        public override int GetHashCode()
        {
            return Size.GetHashCode();
        }
    }
}