using System;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Move : IMove
    {
        public Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Y { get; set; }

        public int X { get; set; }

        public Tuple<IPosition, IMoveInfo> Perform(IPosition position)
        {
            throw new NotImplementedException();
        }
    }
}