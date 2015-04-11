using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Position : IPosition
    {
        private readonly CellField _field;
        private readonly GroupField _groupField;
        private readonly IPlayerProvider _playerProvider;
        private readonly List<Group> _groups;

        public Position(int size, IPlayerProvider playerProvider)
        {
            if (playerProvider == null) throw new ArgumentNullException("playerProvider");
            Contract.EndContractBlock();

            _playerProvider = playerProvider;
            // создаём новое поле для камней и инициализируем его
            _field = new CellField(size);
            var emptyCell = new EmptyCell();
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    _field.SetAt(new TwoDimensionsCoordinates(x, y), emptyCell);
                }
            }

            // создаём новое поле для ссылок на группы (инициализируется нулями)
            _groupField = new GroupField(size);
            _groups = new List<Group>();

            CurrentPlayer = playerProvider.GetFirstPlayer();
        }

        public Position(Position parent)
        {
            var p = parent;
            if (p == null) throw new ArgumentException("parameter should be instance of Position class");
            _field = new CellField(p._field);
            _groupField = new GroupField(p._groupField);
            _groups = new List<Group>(p._groups);
            _playerProvider = p._playerProvider;
            CurrentPlayer = _playerProvider.GetNextPlayer(p.CurrentPlayer);
        }

        public object Clone()
        {
            return new Position(this);
        }

        public ICell GetCellAt(ICoordinates coordinates)
        {
            return _field.GetAt(coordinates);
        }

        public IPlayer CurrentPlayer { get; }

        public void ChangeCellState(ICoordinates coordinates, ICell cell)
        {
            _field.SetAt(coordinates, cell);
        }

        public List<Group> GetNearestGroups(ICoordinates coordinates, IPlayer player)
        {
            var neighbours = coordinates.Neighbours(this).ToArray();
            var enumerable = neighbours.Select(point1 => _groupField.GetAt(point1)).ToArray();
            var enumerable1 = enumerable.Where(grp => grp != null && grp.Player == player).ToArray();
            var groups = enumerable1.Distinct().ToList();
            return groups;
        }

        public void AddGroup(Group grp)
        {
            _groups.Add(grp);
        }

        public void SetGroupAt(ICoordinates coordinates, Group grp)
        {
            _groupField.SetAt(coordinates, grp);
        }

        public void RemoveGroup(Group grp)
        {
            _groups.Remove(grp);
        }

        public void ExcludeGroups(List<Group> groups)
        {
            foreach (var @group in groups)
            {
                _groups.Remove(group);
            }
        }

        public bool Exist(ICoordinates coordinates)
        {
            return _field.Exist(coordinates);
        }

        public IEnumerable<Tuple<ICoordinates, ICell>> CompareStoneField(IPosition position)
        {
            var p = (Position) position;
            foreach (var keyValuePair in _field)
            {
                var cellAt = position.GetCellAt(keyValuePair.Key);
                if (!keyValuePair.Value.Equals(cellAt))
                {
                    yield return Tuple.Create(keyValuePair.Key, cellAt);
                }
            }
        }

        public IEnumerable<Tuple<IPlayer, double>> CompareScore(IPosition position)
        {
            throw new NotImplementedException();
        }

        public double GetScore(IPlayer player)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetGroupsOnPoints(IEnumerable<ICoordinates> oppositePoints)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var b = Equals(other._groups.Count, _groups.Count);
            var b1 = b && Equals(other._field, _field);
            return b1;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Position)) return false;
            var b = Equals((Position) obj);
            return b;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_groups.GetHashCode()*397) ^ _field.GetHashCode();
            }
        }

        public static IPosition CreateInitial(int size, IPlayerProvider playerProvider)
        {
            var position = new Position(size, playerProvider);
            return position;
        }
    }
}