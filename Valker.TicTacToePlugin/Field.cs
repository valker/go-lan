using System;
using Valker.PlayOnLan.Api;

namespace Valker.TicTacToePlugin
{
    /// <summary>
    /// Provide the service of game-field
    /// </summary>
    public class Field
    {
        private Stone[,] _field;

        public Field(int width)
        {
            Width = width;
            _field = new Stone[width,width];
        }

         protected int Width { get; set; }

         public void Set(int x, int y, Stone stone)
        {
            if (stone == Stone.None && _field[x, y] != Stone.None)
                throw new InvalidOperationException("cannot place stone to non-empty place");

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
            return Stone.None;
        }

        private Stone TestWinVertical(int stones)
        {
            for (int x = 0; x < Width; ++x)
            {
                Stone previous = Stone.None;
                int count = 0;
                for (int y = 0; y < Width; y++)
                {
                    Stone current = _field[x, y];
                    if (current == Stone.None) continue;

                    if (current == previous)
                    {
                        ++count;
                        if (count == stones) return current;
                    }
                    else 
                    {
                        count = 1;
                    }

                    previous = current;
                }
            }

            return Stone.None;
        }

        /// <summary>
        /// Check that horizontals contains at least one segment of required length
        /// </summary>
        /// <param name="stones">required length of segment</param>
        /// <returns>Which is the color of the stones, or None otherwise</returns>
        private Stone TestWinHorizontal(int stones)
        {
            for (int y = 0; y < Width; ++y)
            {
                Stone previous = Stone.None;
                int count = 0;
                for(int x = 0; x < Width; ++x)
                {
                    Stone current = _field[x, y];
                    if (current == Stone.None) continue;
                    if (current == previous)
                    {
                        ++count;
                        if (count == stones) 
                        {
                            return current; 
                        }
                    } 
                    else
                    {
                        count = 1;
                    }

                    previous = current;
                }
            }

            return Stone.None;
        }
    }
}