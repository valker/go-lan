using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public class PositionStorage
    {
        private Dictionary<Position, Position> _childToParent = new Dictionary<Position, Position>();
        private Dictionary<Position, ICollection<Position>> _parentToChildren = new Dictionary<Position, ICollection<Position>>();

        public PositionStorage(int size)
        {
            Initial = Position.CreateInitial(size);
        }

        public Position Initial { get; private set; }

        /// <summary>
        /// Произвести ход по правилам Го
        /// </summary>
        /// <param name="originPosition">Исходная позиция</param>
        /// <param name="point">точка, в которую произведен ход</param>
        /// <param name="player">игрок, который сделал ход</param>
        /// <returns>новая позиция</returns>
        public Pair<Position, int> Move(Position originPosition, Point point, MokuState player)
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
        public Position Edit(Position originPosition, Point point, MokuState mokuState)
        {
            Position position;
            // Если исходная позиция "редактируемая", то правим её
            if (originPosition.IsEditable)
            {
                position = originPosition;
            }
            // Иначе создаём новую позицию с таким же положением камней, как и у исходной
            else
            {
                position = originPosition.CopyMokuField();

                // задаём отношение родства
                AddRelationship(originPosition, position);
            }

            // Меняем положение указанной точки (ставим или снимаем камень)
            position.Field.SetAt(point, mokuState);

            // возвращаем отредактированную позицию
            return position;
        }

        private void AddRelationship(Position parent, Position child)
        {
            // добавить ссылку ребёнок -> родитель
            _childToParent.Add(child, parent);

            // добавить ссылку родитель -> ребёнок
            if(!_parentToChildren.ContainsKey(parent))
            {
                _parentToChildren.Add(parent, new List<Position>());
            }
            _parentToChildren[parent].Add(child);
        }

        /// <summary>
        /// Получить родительскую позицию для данной исходной
        /// </summary>
        /// <param name="position">Исходная позиция</param>
        /// <returns>Родительская позиция. null, если данная позиция корневая</returns>
        public Position GetParentPosition(Position position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить дочерние позиции для данной исходной
        /// </summary>
        /// <param name="position">Исходная позиция</param>
        /// <returns>Дочерние позиции для данной исходной</returns>
        public IEnumerable<Position> GetChildPositions(Position position)
        {
            throw new NotImplementedException();
        }
    }
}
