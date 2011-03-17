using System;
using Valker.PlayOnLan.Api;

namespace Valker.TicTacToePlugin
{
    public class Field
    {
        private Stone[,] _field;

        public Field(int width)
        {
            _field = new Stone[width,width];
        }

        public void Set(int x, int y, Stone stone)
        {
            _field[x, y] = stone;
        }

        public Stone Get(int x, int y)
        {
            return _field[x, y];
        }

        public Stone Win(int stones)
        {
            Stone result = TestWinHorizontal(stones);
            if (result != Stone.None) return result;
            result = TestWinVertical(stones);
            if(result != Stone.None) return result;
            result = TestWinDiag(stones);
            if (result != Stone.None) return result;
            return Stone.None;
        }

        private Stone TestWinDiag(int stones)
        {
            throw new NotImplementedException();
        }

        private Stone TestWinVertical(int stones)
        {
            throw new NotImplementedException();
        }

        private Stone TestWinHorizontal(int stones)
        {
            throw new NotImplementedException();
        }
    }
}