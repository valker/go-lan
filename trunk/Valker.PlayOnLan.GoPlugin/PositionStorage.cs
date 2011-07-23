using System;
using System.Collections.Generic;
using System.Linq;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    class PositionStorage
    {
        /// <summary>
        /// First - parent, second - child
        /// </summary>
        List<RelationInfo> _relationship = new List<RelationInfo>();

        public PositionStorage(int size, Rules rules)
        {
            Initial = Position.CreateInitial(size);
            Rules = rules;
        }

        public PositionStorage(IPosition initial, Rules rules)
        {
            Initial = initial;
            Rules = rules;
        }

        public PositionStorage(int size) : this(size, new Rules {Ko = KoRule.SuperPositioned, Score = ScoreRule.EmptyTerritory})
        {
        }

        protected Rules Rules { get; set; }

        /// <summary>
        /// Возвращает исходную позицию в дереве игры
        /// </summary>
        public IPosition Initial { get; private set; }

        public IEnumerable<IPosition> GetChildPositions(IPosition position)
        {
            return _relationship.Where(info => info.Parent.Equals(position)).Select(pair => pair.Child);
        }

        public Pair<IPosition, int> Move(IPosition position, Point point, Stone player)
        {
            // выполняем ход по правилам го и создаём новую позицию
            Pair<IPosition, int> newPosition = position.Move(point, player);

            // todo: check that this situation is not repeated and follow the rules
            // проверяем, что этот ход не повторялся до этого
            var distance = GetPositionDistanceImpl(newPosition.First, position);
            //
            //
            //Rules.Check(newPosition, distance);

            // проверяем, что такого хода ещё не делалось из этой позиции
            var children = GetChildPositions(position);

            bool x = children.Contains(newPosition.First);

            if (!x)
            {
                // добавляем этот ход 
                const bool notPass = false;
                AddRelationship(position, newPosition.First, notPass);
            }

            return newPosition;
        }

        private void AddRelationship(IPosition parent, IPosition child, bool isPass)
        {
            IMove move = isPass ? (IMove) new Pass() : new Move();
            var relation = new RelationInfo {Parent = parent, Child = child, Move = move};
            _relationship.Add(relation);
        }

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

        private IPosition GetParentPosition(IPosition position)
        {
            var relationInfo = _relationship.Where(info => ReferenceEquals(info.Child, position)).FirstOrDefault();
            if (relationInfo == null)
            {
                return null;
            }

            return relationInfo.Parent;
        }

        public IPosition Edit(IPosition initial, Point point, Stone stone)
        {
            throw new NotImplementedException();
        }
    }
}
