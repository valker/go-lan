using System;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public class MokuField : IEquatable<MokuField>
    {
        private readonly MokuState[,] _field;
        public int Size { get; private set; }

        public MokuField(int size)
        {
            if (size < 2)
            {
                throw new ArgumentException("Size should be greather than 1", "size");
            }
            Size = size;
            _field = new MokuState[size, size];
        }

        public MokuField(MokuField parent)
        {
            _field = (MokuState[,]) parent._field.Clone();
            Size = parent.Size;
        }

        public MokuState GetAt(Point point)
        {
            return _field[point.Y, point.X];
        }

        public void SetAt(Point point, MokuState value)
        {
            _field[point.Y, point.X] = value;
        }

        public bool Equals(MokuField other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (other.Size != Size)
            {
                return false;
            }

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (_field[y,x] != other._field[y,x])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof (MokuField))
            {
                return false;
            }
            return Equals((MokuField) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_field != null ? _field.GetHashCode() : 0) * 397) ^ Size;
            }
        }
    }
}
