using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
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

        /// <summary>
        /// Модифицировать поле без учёта правил. Т.е. можно поставить камень, полностью окружённый противником.
        /// </summary>
        /// <param name="originPosition">Исходная позиция</param>
        /// <param name="point">Точка, которую меняем</param>
        /// <param name="mokuState">новое состояние точки</param>
        /// <returns>новая (или текущая) позиция</returns>
        private Position Edit(Position originPosition, Point point, MokuState mokuState)
        {
            throw new NotImplementedException();
        }
    }
}
