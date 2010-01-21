using System;
using System.Linq;
using System.Text;

namespace go_engine.Data
{
    public class Position
    {
        public MokuField Field { get; private set; }
        private GroupCollection _groups = new GroupCollection();

        private Position(int size)
        {
            Field = new MokuField(size);
        }

        public int Size { get { return Field.Size; } }

        public bool IsEditable { get; set; }

        public static Position CreateInitial(int size)
        {
            var position = new Position(size);
            position.IsEditable = true;
            return position;
        }

        public Position CopyMokuField()
        {
            var position = new Position(Size);
            position.Field = new MokuField(Field);
            position.IsEditable = true;
            return position;
        }

        /// <summary>
        /// Выполняет ход по правилам Го
        /// </summary>
        /// <param name="point">точка, в которую ходят</param>
        /// <param name="player">игрок, который делает ход</param>
        /// <param name="Rules">особенности правил</param>
        /// <returns>новая позиция и число съеденных камней</returns>
        internal Pair<Position, int> Move(Microsoft.Xna.Framework.Point point, MokuState player, Rules Rules)
        {
            throw new NotImplementedException();
        }
    }
}
