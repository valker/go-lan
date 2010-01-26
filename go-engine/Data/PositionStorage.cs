using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public class PositionStorage
    {
        private Dictionary<IPosition, IPosition> _childToParent = new Dictionary<IPosition, IPosition>();
        private Dictionary<IPosition, ICollection<IPosition>> _parentToChildren = new Dictionary<IPosition, ICollection<IPosition>>();

        public Rules Rules { get; private set; }

        public PositionStorage(int size)
        {
            Initial = Position.CreateInitial(size);
            OnAddNewPosition(Initial);
            Rules = new Rules();
            Rules.Ko = KoRule.SuperPositioned;
            Rules.Points = Points.Empty;
        }

        public IPosition Initial { get; private set; }

        /// <summary>
        /// Произвести ход по правилам Го
        /// </summary>
        /// <param name="originPosition">Исходная позиция</param>
        /// <param name="point">точка, в которую произведен ход</param>
        /// <param name="player">игрок, который сделал ход</param>
        /// <returns>новая позиция</returns>
        public Pair<IPosition, int> Move(IPosition originPosition, Point point, MokuState player)
        {
            // выполняем ход по правилам го и создаём новую позицию
            Pair<IPosition, int> newPosition = originPosition.Move(point, player);

            // проверяем, что такого хода ещё не делалось из этой позиции
            var children = GetChildPositions(originPosition);

            bool x = children.Contains(newPosition.First, new DDD());

            if (!x)
            {
                // добавляем этот ход 
                AddRelationship(originPosition, newPosition.First);
            }

            return newPosition;
        }

        class DDD : IEqualityComparer<IPosition>
        {
            public bool Equals(IPosition left, IPosition right)
            {
                throw new NotImplementedException();
            }
            public int GetHashCode(IPosition position)
            { 
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Модифицировать поле без учёта правил. Т.е. можно поставить камень, полностью окружённый противником.
        /// </summary>
        /// <param name="originPosition">Исходная позиция</param>
        /// <param name="point">Точка, которую меняем</param>
        /// <param name="mokuState">новое состояние точки</param>
        /// <returns>новая (или текущая) позиция</returns>
        public IPosition Edit(IPosition originPosition, Point point, MokuState mokuState)
        {
            IPosition position;
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

        /// <summary>
        /// Получить родительскую позицию для данной исходной
        /// </summary>
        /// <param name="position">Исходная позиция</param>
        /// <returns>Родительская позиция.
        /// null, если данная позиция корневая</returns>
        public IPosition GetParentPosition(IPosition position)
        {
            IPosition parent;
            parent = _childToParent.TryGetValue(position, out parent) ? parent : null;
            return parent;
        }

        /// <summary>
        /// Получить дочерние позиции для данной исходной
        /// </summary>
        /// <param name="position">Исходная позиция</param>
        /// <returns>Дочерние позиции для данной исходной</returns>
        public IEnumerable<IPosition> GetChildPositions(IPosition position)
        {
            return _parentToChildren[position];
        }

        private void OnAddNewPosition(IPosition position)
        {
            _parentToChildren.Add(position, new List<IPosition>());
        }

        private void AddRelationship(IPosition parent, IPosition child)
        {
            // добавить ссылку ребёнок -> родитель
            _childToParent.Add(child, parent);

            // добавить контейнер для детей нового ребёнка
            OnAddNewPosition(child);

            // добавить ссылку родитель -> ребёнок
            _parentToChildren[parent].Add(child);
        }
    }
}
