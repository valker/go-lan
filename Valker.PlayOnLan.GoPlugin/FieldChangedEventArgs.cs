using System;
using System.Linq;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    public class FieldChangedEventArgs : EventArgs
    {
        private int[] _coordinates;
        public Stone Stone { get; private set; }
        public int X { get { return _coordinates[0]; } }
        public int Y { get { return _coordinates[1]; } }

        public FieldChangedEventArgs(Tuple<Point,Stone> pair)
        {
            _coordinates = new int[]{pair.Item1.X, pair.Item1.Y};
            Stone = pair.Item2;
        }

        public FieldChangedEventArgs(string[] strings)
        {
            _coordinates = strings.Take(2).Select(s => int.Parse(s)).ToArray();
            Stone = (Stone) Enum.Parse(typeof (Stone), strings[2]);
        }
    }
}