using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api;

namespace Valker.PlayOnLan.GoPlugin
{
    class Field
    {
        readonly Stone[,] _field;
        public Field(int width)
        {
            _field = new Stone[width,width];
        }

        public Stone Get(int x, int y)
        {
            return _field[x, y];
        }

        public void Set(int x, int y, Stone stone)
        {
            _field[x, y] = stone;
        }
    }
}
