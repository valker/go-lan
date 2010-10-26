using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public class PositionStorage
    {
        //private Dictionary<IPosition, IPosition> _childToParent = new Dictionary<IPosition, IPosition>();
        //private Dictionary<IPosition, ICollection<IPosition>> _parentToChildren = new Dictionary<IPosition, ICollection<IPosition>>();

        private List<IPosition> _positions = new List<IPosition>();

        /// <summary>
        /// Возвращает исходную позицию в дереве игры
        /// </summary>
        public IPosition Initial { get; private set; }

        public Rules Rules { get; private set; }

        /// <summary>
        /// Конструктор, используемый для создания начальной позиции в игре
        /// </summary>
        /// <param name="size"></param>
        public PositionStorage(int size)
        {
            Initial = Position.CreateInitial(size);
            OnAddNewPosition(Initial);
            Rules = new Rules();
            Rules.Ko = KoRule.SuperPositioned;
            Rules.Points = Points.Empty;
        }

        /// <summary>
        /// Конструктор, используемый для создания предопределённой позиции как стартовой
        /// </summary>
        /// <param name="initialPosition"></param>
        /// <param name="goRules"></param>
        public PositionStorage(IPosition initialPosition, Rules goRules)
        {
            Initial = initialPosition;
            OnAddNewPosition(Initial);
            Rules = goRules;
        }


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

            // проверяем, что этот ход не повторялся до этого
            var distance = GetPositionDistanceImpl(newPosition.First, originPosition);

            Rules.Check(newPosition, distance);

            // проверяем, что такого хода ещё не делалось из этой позиции
            var children = GetChildPositions(originPosition);

            bool x = children.Contains(newPosition.First);

            if (!x)
            {
                // добавляем этот ход 
                AddRelationship(originPosition, newPosition.First);
            }

            return newPosition;
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
            if (position == null)
            {
                throw new ArgumentNullException("position");
            }

            return position.GetParentPosition();
        }

        /// <summary>
        /// Получить дочерние позиции для данной исходной
        /// </summary>
        /// <param name="position">Исходная позиция</param>
        /// <returns>Дочерние позиции для данной исходной</returns>
        public IEnumerable<IPosition> GetChildPositions(IPosition position)
        {
            if (position == null)
            {
                throw new ArgumentNullException("position");
            }

            return position.GetChildrenPositions();
        }

        /// <summary>
        /// Определить расстояние от текущей позиции, до позиции равной заданной
        /// </summary>
        /// <param name="mostYoungestChild">с этой позицией проверяется равность</param>
        /// <param name="currentChild">позиция, расстояние от которой расчитывается</param>
        /// <returns></returns>
        private Pair<int, IPosition> GetPositionDistanceImpl(IPosition mostYoungestChild, IPosition currentChild)
        {
            // получить родительскую позицию для текущей
            var parent = GetParentPosition(currentChild);

            // если родительская позиция существует
            if (parent != null)
            {
                // сравнить её с базой для сравнения
                if (parent.Equals(mostYoungestChild))
                {
                    // если равно, вернуть расстояние равное 2
                    return new Pair<int, IPosition>(2, parent);
                }
                // иначе расчитать расстояние от родительской до равной заданной
                var distance = GetPositionDistanceImpl(mostYoungestChild, parent);
                // если достижимо, вернуть (расстояние + 1), если нет, вернуть константу (-1)
                return distance.First == -1 ? distance : new Pair<int, IPosition>(distance.First + 1, distance.Second);
            }
            // недостижимо равенство. возвращаем константу (-1)
            return new Pair<int, IPosition>(-1, null);
        }

        /// <summary>
        /// Произвести действия при добавлении новой позиции в дерево партии
        /// </summary>
        /// <param name="position">новая позиция</param>
        private void OnAddNewPosition(IPosition position)
        {
            _positions.Add(position);
            //_parentToChildren.Add(position, new List<IPosition>());
        }

        /// <summary>
        /// добавить отношение между позициями
        /// </summary>
        /// <param name="parent">родительская позиция</param>
        /// <param name="child">дочерняя позиция</param>
        private void AddRelationship(IPosition parent, IPosition child)
        {
            parent.AddChild(child);
            child.SetParent(parent);
            
            //// добавить ссылку ребёнок -> родитель
            //_childToParent.Add(child, parent);

            //// добавить контейнер для детей нового ребёнка
            //OnAddNewPosition(child);

            //// добавить ссылку родитель -> ребёнок
            //_parentToChildren[parent].Add(child);
        }
    }
}
