using System;
using System.Collections.Generic;
using System.Linq;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    public class PositionStorage : IPositionStorage
    {
        /// <summary>
        /// First - parent, second - child
        /// </summary>
        readonly List<RelationInfo> _relationship = new List<RelationInfo>();

        public PositionStorage(int size, IPlayerProvider playerProvider)
        {
            Initial = Position.CreateInitial(size, playerProvider);
        }

        public PositionStorage(IPosition initial)
        {
            Initial = initial;
//            Rules = rules;
        }

//        public PositionStorage(int size) : this(size)
//        {
//        }

//        protected IRules Rules { get; set; }

        /// <summary>
        /// Возвращает исходную позицию в дереве игры
        /// </summary>
        public IPosition Initial { get; }

//        public IEnumerable<IPosition> GetChildPositions(IPosition position)
//        {
//            return _relationship.Where(info => info.Parent.Equals(position)).Select(pair => pair.Child);
//        }

//        public Tuple<IPosition, IMoveInfo> Move(IPosition position, Point point, Stone player)
//        {
//            // выполняем ход по правилам го и создаём новую позицию
//            Tuple<IPosition, IMoveInfo> newPosition = position.Move(point, player);
//
//            // todo: check that this situation is not repeated and follow the rules
//            // проверяем, что этот ход не повторялся до этого
//            Pair<int, IPosition> distance = GetPositionDistanceImpl(newPosition.Item1, position);
//            //
//            //
//            //Rules.Check(newPosition, distance);
//            bool isAcceptable = Rules.IsAcceptable(newPosition, distance);
//
//            // проверяем, что такого хода ещё не делалось из этой позиции
//            var children = GetChildPositions(position);
//
//            bool x = children.Contains(newPosition.Item1);
//
//            if (!x)
//            {
//                // добавляем этот ход 
//                AddRelationship(position, newPosition.Item1);
//            }
//
//            return newPosition;
//        }

/*
        private void AddRelationship(IPosition parent, IPosition child)
        {
            IMove move = new Move();
            var relation = new RelationInfo {Parent = parent, Child = child, Move = move};
            _relationship.Add(relation);
        }
*/

/*
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
                return distance.Item1 == -1 ? distance : new Pair<int, IPosition>(distance.Item1 + 1, distance.Item2);
            }
            // недостижимо равенство. возвращаем константу (-1)
            return new Pair<int, IPosition>(-1, null);
        }
*/

/*
        private IPosition GetParentPosition(IPosition position)
        {
            var relationInfo = _relationship.FirstOrDefault(info => ReferenceEquals(info.Child, position));

            return relationInfo?.Parent;
        }
*/

/*
        public IPosition Edit(IPosition initial, Point point, Stone stone)
        {
            throw new NotImplementedException();
        }
*/
    }
}
