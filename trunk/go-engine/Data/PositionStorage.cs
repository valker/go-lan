using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    struct Pair<T1, T2>
    {
    }

    class PositionStorage
    {
        /// <summary>
        /// Произвести ход по правилам Го
        /// </summary>
        /// <param name="originPosition">Исходная позиция</param>
        /// <param name="point">точка, в которую произведен ход</param>
        /// <param name="player">игрок, который сделал ход</param>
        /// <returns>новая позиция</returns>
        Pair<Position, int> Move(Position originPosition, Point point, MokuState player)
        {
            throw new NotImplementedException();
        }

        private Position Edit(Position originPosition, Point point, MokuState mokuState)
        {
            throw new NotImplementedException();
        }
    }
}
