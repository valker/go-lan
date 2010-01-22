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
            int size = Size;

            if (size != other.Size)
            {
                return false;
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (_field[i,j] != other._field[i,j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
