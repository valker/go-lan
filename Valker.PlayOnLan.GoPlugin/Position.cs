using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Position : IPosition
    {
        private readonly CellField _field;
        private readonly GroupField _groupField;
        private readonly IPlayerProvider _playerProvider;
        private readonly List<IGroup> _groups;
        private readonly Dictionary<IPlayer, double> _score; 

        public Position(int size, IPlayerProvider playerProvider, ICoordinatesFactory coordinatesFactory)
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
                    _field.SetAt(coordinatesFactory.Create(new []{x, y}), emptyCell);
                }
            }

            // создаём новое поле для ссылок на группы (инициализируется нулями)
            _groupField = new GroupField(size);
            _groups = new List<IGroup>();

            _score = playerProvider.GetPlayers().ToDictionary(player => player, player => 0.0);

            CurrentPlayer = playerProvider.GetFirstPlayer();
        }

        public Position(Position parent)
        {
            var p = parent;
            if (p == null) throw new ArgumentException("parameter should be instance of Position class");
            _field = new CellField(p._field);
            _groupField = new GroupField(p._groupField);
            _groups = new List<IGroup>(p._groups);
            _playerProvider = p._playerProvider;
            _score = new Dictionary<IPlayer, double>(p._score);
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
            foreach (var player in _playerProvider.GetPlayers())
            {
                var previousScore = GetScore(player);
                var newScore = position.GetScore(player);
                if (newScore != previousScore)
                {
                    yield return Tuple.Create(player, newScore);
                }
            }
        }

        public double GetScore(IPlayer player)
        {
            return _score[player];
        }

        private IEnumerable<Group> GetGroupsOnPoints(IEnumerable<ICoordinates> oppositePoints)
        {
            return oppositePoints.Select(coordinates => _groupField.GetAt(coordinates)).OrderBy(@group => @group).Distinct();
        }

        private bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var b = Equals(other._groups.Count, _groups.Count);
            var b1 = b && Equals(other._field, _field);
            var b2 = b1 && Equals(other.CurrentPlayer, CurrentPlayer);
            return b2;
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

        public static IPosition CreateInitial(int size, IPlayerProvider playerProvider, ICoordinatesFactory coordinatesFactory)
        {
            var position = new Position(size, playerProvider, coordinatesFactory);
            return position;
        }

        public IMoveConsequences MoveConsequences(ICoordinates coordinates)
        {
            ICell state = GetCellAt(coordinates);

            if (!(state is EmptyCell))
            {
                throw new GoException(ExceptionReason.Occuped);
            }
            // создать новую позицию, как копию исходной
            var newPosition = (Position) (Clone());

            // поставить камень, определить, в какую группу он входит
            IGroup @group = newPosition.PutStone(coordinates, CurrentPlayer);

            // удалить соседние группы, которые остались без дыханий
            IPlayer[] opposite = Util.Opposite(@group.Player, _playerProvider);

            int stoneCount = newPosition.RemoveDeathOppositeGroups(@group, opposite);

            // проверить живость новой группы
            if (!newPosition.CheckIsLive(@group))
            {
                throw new GoException(ExceptionReason.SelfDead);
            }

            newPosition._score[CurrentPlayer] += stoneCount;

            return new MoveConsequences {Position = newPosition, ScoreDelta = stoneCount};
        }

        public IGroup PutStone(ICoordinates coordinates, IPlayer currentPlayer)
        {
            // поставить точку на поле
            ChangeCellState(coordinates, new PlayerCell(currentPlayer));

            // обновить группы для новой позиции
            var group = UpdateGroups(coordinates, currentPlayer);
            return @group;
        }

        private int RemoveDeathOppositeGroups(IGroup grp, IPlayer[] opposite)
        {
            // находим соседние группы противника
            IEnumerable<Group> oppositeGroups = FindOppositeGroup(grp, opposite);

            var groupsToKill = oppositeGroups.Where(g => !CheckIsLive(g));

            int count = 0;

            // для всех групп из списка "на удаление"
            foreach (Group groupToKill in groupsToKill)
            {
                // добавляем количество камней в группе к количеству снятых с доски камней
                count += groupToKill.Count;
                // убираем группу
                RemoveGroup(groupToKill);
                // модифицируем поле
                foreach (var pnt in groupToKill)
                {
                    ChangeCellState(pnt, new EmptyCell());
                    SetGroupAt(pnt, null);
                }
            }

            // возвращаем количество снятых камней
            return count;
        }

        private bool CheckIsLive(IGroup grp)
        {
            var dame = GetDame(grp).ToArray();
            return dame.Take(1).Any();
        }

        private Group UpdateGroups(ICoordinates coordinates, IPlayer currentPlayer)
        {
            // получить соседние группы того же цвета
            List<Group> groups = GetNearestGroups(coordinates, currentPlayer);

            Group group;
            switch (groups.Count)
            {
                case 0: // если их нет
                    // создаём новую группу
                    @group = new Group(coordinates, currentPlayer);
                    AddGroup(@group);
                    SetGroupAt(coordinates, @group);
                    return @group;
                case 1: // если одна группа
                    Group grp = groups[0].AddPoint(coordinates);
                    foreach (var pnt in grp)
                    {
                        SetGroupAt(pnt, grp);
                    }
                    RemoveGroup(groups[0]);
                    AddGroup(grp);
                    return grp;
                default: // если больше одной группы
                    @group = groups[0];
                    for (var i = 1; i < groups.Count; i++)
                    {
                        foreach (var pnt in groups[i])
                        {
                            @group = @group.AddPoint(pnt);
                        }
                    }
                    ExcludeGroups(groups);

                    @group = @group.AddPoint(coordinates);
                    AddGroup(@group);
                    foreach (var pnt in @group)
                    {
                        SetGroupAt(pnt, @group);
                    }
                    return @group;
            }
        }

        private IEnumerable<ICoordinates> GetDame(IGroup grp)
        {
            return grp.SelectMany(delegate(ICoordinates point)
            {
                var coordinateses = GetDame(point).ToArray();
                return coordinateses;
            });
        }

        public IEnumerable<ICoordinates> GetDame(ICoordinates point)
        {
            IEnumerable<ICoordinates> coordinateses = point.Neighbours(this).ToArray();
            return coordinateses.Where(pnt => GetCellAt(pnt) is EmptyCell);
        }

        /// <summary>
        /// Найти группы противника примыкающие к указанной
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="players"></param>
        /// <returns></returns>
        private IEnumerable<Group> FindOppositeGroup(IGroup grp, IPlayer[] players)
        {
            List<Group> allGroups = new List<Group>();
            foreach (var player in players)
            {
                // координаты соседних точек, в которых находятся камни противника
                var player1 = player;
                IEnumerable<ICoordinates> oppositePoints = grp.SelectMany(point => point.Neighbours(this)).Where(point => GetCellAt(point).Equals(new PlayerCell(player1))).OrderBy(coordinates => coordinates).Distinct();
                oppositePoints = oppositePoints.ToArray();
                // группы противника, примыкающие к указанной точке
                IEnumerable<Group> opposGroups = GetGroupsOnPoints(oppositePoints);
                allGroups.AddRange(opposGroups);
            }

            return allGroups;
        }
    }
}