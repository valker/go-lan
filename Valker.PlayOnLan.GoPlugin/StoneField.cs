using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    public class StoneField
    {
        private readonly Stone[,] _field;

        public StoneField(StoneField parent)
        {
            _field = (Stone[,]) parent._field.Clone();
            Size = parent.Size;
        }

        public StoneField(int size)
        {
            Size = size;
            _field = new Stone[size,size];
        }

        public int Size { get; private set; }

        public void SetAt(Point point, Stone stone)
        {
            _field[point.X, point.Y] = stone;
        }

        public Stone GetAt(Point point)
        {
            return _field[point.X, point.Y];
        }

        public bool Equals(StoneField other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (other._field[x,y] != _field[x,y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (StoneField)) return false;
            return Equals((StoneField) obj);
        }

        public override int GetHashCode()
        {
            return Size.GetHashCode();
        }
    }
}