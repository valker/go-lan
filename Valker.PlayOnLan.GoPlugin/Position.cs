using System;
using System.Collections.Generic;
using System.Linq;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Position : IPosition
    {
        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var b = Equals(other.Groups.Count, Groups.Count);
            var b1 = b && Equals(other.Field, Field);
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
                return (Groups.GetHashCode()*397) ^ Field.GetHashCode();
            }
        }

        public object Clone()
        {
            return new Position(this);
        }

        private readonly GroupField _groupField;
        public List<Group> Groups { get; } = new List<Group>();

        private Position(int size, IPlayerProvider playerProvider)
        {
            // создаём новое поле для камней и инициализируем его
            Field = new CellField(size);
            var emptyCell = new EmptyCell();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Field.SetAt(new TwoDimensionsCoordinates(x,y), emptyCell);
                }
            }

            // создаём новое поле для ссылок на группы (инициализируется нулями)
            _groupField = new GroupField(size);

            CurrentPlayer = playerProvider.GetFirstPlayer();
            PlayerProvider = playerProvider;
        }

        private IPlayerProvider PlayerProvider { get; set; }

        public Position(Position parent)
        {
            Position p = parent as Position;
            if(p == null) throw new ArgumentException("parameter should be instance of Position class");
            Field = new CellField(p.Field);
            _groupField = new GroupField(p._groupField);
            Groups = new List<Group>(p.Groups);
            PlayerProvider = p.PlayerProvider;
            CurrentPlayer = PlayerProvider.GetNextPlayer(p.CurrentPlayer);
        }

        protected CellField Field { get; set; }

        public static IPosition CreateInitial(int size, IPlayerProvider playerProvider)
        {
            var position = new Position(size, playerProvider);
            return position;
        }

        public ICell GetCellAt(ICoordinates coordinates)
        {
            return Field.GetAt(coordinates);
        }

        public IPlayer CurrentPlayer
        {
            get; }

        public void ChangeCellState(ICoordinates coordinates, ICell cell)
        {
            Field.SetAt(coordinates, cell);
        }

        public List<Group> GetNearestGroups(ICoordinates coordinates, IPlayer player)
        {
            ICoordinates[] neighbours = coordinates.Neighbours(this).ToArray();
            var enumerable = neighbours.Select(point1 => _groupField.GetAt(point1)).ToArray();
            var enumerable1 = enumerable.Where(grp => grp != null && grp.Player == player).ToArray();
            List<Group> groups = enumerable1.Distinct().ToList();
            return groups;
        }

        public void AddGroup(Group grp)
        {
            Groups.Add(grp);
        }

        public void SetGroupAt(ICoordinates coordinates, Group grp)
        {
            _groupField.SetAt(coordinates, grp);
        }

        public void RemoveGroup(Group grp)
        {
            throw new NotImplementedException();
        }

        public void ExcludeGroups(List<Group> groups)
        {
            throw new NotImplementedException();
        }

        public bool Exist(ICoordinates coordinates)
        {
            return Field.Exist(coordinates);
        }

/*
        public void ChangeCellState(ICoordinates coordinates, IPlayer currentPlayer)
        {
            throw new NotImplementedException();
        }
*/

//        public Tuple<IPosition, IMoveInfo> Move(Point point, IPlayer player)
//        {
//            var state = Field.GetAt(point);
//            if (!(state is EmptyCell))
//            {
//                throw new GoException(ExceptionReason.Occuped);
//            }
//            // создать новую позицию, как копию исходной
//            var position = new Position(this);
//
//            // поставить точку на поле
//            position.Field.SetAt(point, new PlayerCell(player));
//
//            // обновить группы для новой позиции
//            var group = UpdateGroups(position, point, player, IsEditable);
//
//            // удалить соседние группы, которые остались без дыханий
//            int stoneCount = RemoveDeathOppositeGroups(position, group);
//
//            // проверить живость новой группы
//            if (!CheckIsLive(group, position))
//            {
//                throw new GoException(ExceptionReason.SelfDead);
//            }
//
//            return Tuple.Create<IPosition, IMoveInfo>(position, new MoveInfo(stoneCount));
//        }

/*
        public IEnumerable<Tuple<Point, Stone>> CompareStoneField(IPosition other)
        {
            var size = Field.Size;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    var point = new Point(x,y);
                    var stone = other.GetCellAt(point);
                    if (GetCellAt(point) != stone)
                    {
                        yield return Tuple.Create(point, stone);
                    }
                }
            }
        }
*/

//        public IPlayer GetCellAt(Point point)
//        {
//            return Field.GetAt(point);
//        }

/*
        public int Size
        {
            get { return Field.Size; }
        }
*/

/*
        /// <summary>
        /// Проверяем, что группа жива
        /// </summary>
        /// <param name="grp">группа, которая подлажит проверке</param>
        /// <param name="position">позиция</param>
        /// <returns>true - группа жива</returns>
        private static bool CheckIsLive(Group grp, Position position)
        {
            var dame = GetDame(grp, position);
            return dame.Take(1).Count() > 0;
        }
*/

/*
        private static IEnumerable<ICoordinates> GetDame(Group grp, IPosition position)
        {
            return grp.SelectMany(point => GetDame(point, position));
        }
*/

/*
        private static IEnumerable<ICoordinates> GetDame(ICoordinates point, IPosition position)
        {
            return point.Neighbours(position).Where(pnt => position.GetCellAt(pnt) is EmptyCell);
        }
*/

/*
        /// <summary>
        /// Удалить мёртвые группы противника
        /// </summary>
        /// <param name="position">позиция, в которой работаем</param>
        /// <param name="grp">группа, которая сделала ход</param>
        /// <returns>количество снятых камней</returns>
        private static int RemoveDeathOppositeGroups(Position position, Group grp)
        {
            // находим соседние группы противника
            var oppositeGroups = FindOppositeGroup(position, grp);

            var toKill = oppositeGroups.Where(g => !CheckIsLive(g, position)).ToArray();

            int count = 0;

            // для всех групп из списка "на удаление"
            foreach (Group toKillGrp in toKill)
            {
                // добавляем количество камней в группе к количеству снятых с доски камней
                count += toKillGrp.Count;
                // убираем группу
                position._groups.Remove(toKillGrp);
                // модифицируем поле
                foreach (var pnt in toKillGrp)
                {
                    position.Field.SetAt(pnt, Stone.None);
                    position._groupField.SetAt(pnt, null);
                }
            }
            // возвращаем количество снятых камней
            return count;
        }
*/

/*
        /// <summary>
        /// Найти группы противника, соседствующие с данной группой
        /// </summary>
        /// <param name="position">позиция</param>
        /// <param name="grp">группа</param>
        /// <returns></returns>
        private static IEnumerable<Group> FindOppositeGroup(Position position, Group grp)
        {
            Stone player = Util.Opposite(grp.Player);
            var oppositePoints = grp.SelectMany(point => point.Neighbours(position.Field.Size)).Where(point => position.Field.GetAt(point) == player);
            oppositePoints = oppositePoints.ToArray();
            var opposGroups = position._groups.Where(g => g.Intersect(oppositePoints).Count() > 0);
            opposGroups = opposGroups.ToArray();
            return opposGroups;
        }
*/



/*
        /// <summary>
        /// Обновить данные по группам для нового состояния поля
        /// </summary>
        /// <param name="position"></param>
        /// <param name="point"></param>
        /// <param name="player"></param>
        /// <param name="isParentEditable"></param>
        /// <returns></returns>
        private Group UpdateGroups(Position position, Point point, Stone player, bool isParentEditable)
        {
            // скопировать группы, если исходная игровая или
            // создать группы, если исходная редактируемая
            if (isParentEditable)
            {
                // создать группы на основе поля
                position._groups = MakeGroupsFromField(position);
                foreach (Group grp in position._groups)
                {
                    foreach (var pnt in grp)
                    {
                        position._groupField.SetAt(pnt, grp);
                    }
                }

                foreach (Group grp in position._groups)
                {
                    if (grp.Contains(point))
                    {
                        return grp;
                    }
                }
                throw new InvalidOperationException("Cannot detect which group contains new stone");
            }
            else
            {
                position._groups = CopyGroups();

                // получить соседние группы того же цвета
                var groups = position.GetNearestGroups(point, player);

                Group group;
                switch (groups.Count)
                {
                    case 0: // если их нет
                        // создаём новую группу
                        group = new Group(point, player);
                        position._groups.Add(group);
                        position._groupField.SetAt(point, group);
                        return group;
                    case 1: // если одна группа
                        var grp = groups[0].AddPoint(point);
                        foreach (var pnt in grp)
                        {
                            position._groupField.SetAt(pnt, grp);
                        }
                        position._groups.Remove(groups[0]);
                        position._groups.Add(grp);
                        return grp;
                    default: // если больше одной группы
                        group = groups[0];
                        for (int i = 1; i < groups.Count; i++)
                        {
                            foreach (Point pnt in groups[i])
                            {
                                group = group.AddPoint(pnt);
                            }
                        }
                        position._groups = position._groups.Except(groups).ToList();

                        group = group.AddPoint(point);
                        position._groups.Add(group);
                        foreach (var pnt in group)
                        {
                            position._groupField.SetAt(pnt, group);
                        }
                        return group;
                }
            }
        }
*/
/*
        private List<Group> GetNearestGroups(Point point, Stone player)
        {
            IEnumerable<Point> neighbours = point.Neighbours(Field.Size).ToArray();
            var enumerable = neighbours.Select(point1 => _groupField.GetAt(point1)).ToArray();
            var enumerable1 = enumerable.Where(grp => grp != null && grp.Player == player).ToArray();
            List<Group> groups = enumerable1.Distinct().ToList();
            return groups;
        }
*/

/*
        private List<Group> CopyGroups()
        {
            return new List<Group>(_groups);
        }
*/

/*
        private List<Group> MakeGroupsFromField(Position position)
        {
            // создать коллекцию групп
            var collection = new List<Group>();

            // создать список камней
            var points = GeneratePoints(position.Field);

            // пока список камней не пуст
            while (points.Count > 0)
            {
                // создать группу из близлежащих камней
                Group item = ExtractGroup(points, position.Field);

                // добавить группу в коллекцию
                collection.Add(item);
            }

            // вернуть коллекцию
            return collection;
        }
*/

/*
        /// <summary>
        /// Выделить группу камней и удалить точки из списка
        /// </summary>
        /// <param name="points">точки, указывающие на камни</param>
        /// <param name="field">поле</param>
        /// <returns></returns>
        private static Group ExtractGroup(List<Pair<Point, Stone>> points, CellField field)
        {
            var point = points[0];
            var group = new Group(point.Item1, point.Item2);
            RecourseExtractGroup(group, point, points);
            return group;
        }
*/

/*
        private static void RecourseExtractGroup(Group grp, Pair<Point, Stone> point, List<Pair<Point, Stone>> points)
        {
            var selectedNeighbour = points.Where(pair =>
            {
                if (point.Item2 != pair.Item2)
                {
                    return false;
                }
                return point.Item1.Distance(pair.Item1) <= 1;
            }).ToArray();

            foreach (var pair in selectedNeighbour)
            {
                if (!grp.Contains(pair.Item1))
                {
                    grp.Add(pair.Item1);
                }
                points.Remove(pair);
            }

            foreach (var pair in selectedNeighbour)
            {
                RecourseExtractGroup(grp, pair, points);
            }
        }
*/


/*
        /// <summary>
        /// Создать список точек, которые указывают на камни
        /// </summary>
        /// <param name="field"></param>
        /// <returns>Список точек, которые указывают на камни</returns>
        private static List<Pair<Point, Stone>> GeneratePoints(CellField field)
        {
            var points = new List<Pair<Point, Stone>>();
            for (int x = 0; x < field.Size; x++)
            {
                for (int y = 0; y < field.Size; y++)
                {
                    var point = new Point(x, y);
                    Stone stone = field.GetAt(point);
                    switch (stone)
                    {
                        case Stone.Black:
                        case Stone.White:
                            points.Add(new Pair<Point, Stone>(point, stone));
                            break;
                    }
                }
            }

            return points;
        }
*/

    }

    public class MoveInfo : IMoveInfo
    {
        public MoveInfo(int eated)
        {
            Eated = eated;
        }

        public int Eated { get; }
    }

    public interface IMoveInfo
    {
        int Eated { get; }
    }
}
