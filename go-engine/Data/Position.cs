using System.Linq;
using System.Text;

namespace go_engine.Data
{
    class Position
    {
        private MokuField _field;
        private GroupCollection _groups = new GroupCollection();

        private Position(int size)
        {
            _field = new MokuField(size);
        }

        public int Size { get { return _field.Size; } }

        static Position CreateInitial(int size)
        {
            Position position = new Position(size);
            return position;
        }
    }
}
