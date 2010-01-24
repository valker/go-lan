using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public class Position
    {
        public MokuField Field { get; private set; }
        private List<Group> _groups = new List<Group>();

        private Position(int size)
        {
            Field = new MokuField(size);
        }

        public int Size { get { return Field.Size; } }

        public bool IsEditable { get; set; }

        public static Position CreateInitial(int size)
        {
            var position = new Position(size);
            position.IsEditable = true;
            return position;
        }

        public Position CopyMokuField()
        {
            var position = new Position(Size);
            position.Field = new MokuField(Field);
            position.IsEditable = true;
            return position;
        }

        /// <summary>
        /// Выполняет ход по правилам Го
        /// </summary>
        /// <param name="point">точка, в которую ходят</param>
        /// <param name="player">игрок, который делает ход</param>
        /// <returns>новая позиция и число съеденных камней</returns>
        internal Pair<Position, int> Move(Point point, MokuState player)
        {
            // создать новую позицию, как копию исходной
            var position = new Position(Size);
            position.Field = new MokuField(Field);

            // поставить точку на поле
            position.Field.SetAt(point, player);

            // обновить группы для новой позиции
            var group = UpdateGroups(position, point, player);

            // удалить соседние группы, которые остались без дыханий
            int stoneCount = RemoveDeathOppositeGroups(position, group);

            return new Pair<Position, int>(position, stoneCount);
        }

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
                // TODO: modify the field
            }
            // возвращаем количество снятых камней
            return count;
        }

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

        private static IEnumerable<Point> GetDame(Group grp, Position position)
        {
            return grp.Points.SelectMany(point => GetDame(point, position));
        }

        private static IEnumerable<Point> GetDame(Point point, Position position)
        {
            var neighbourPoints = GetNeighbourPoints(point, position.Size);
            return neighbourPoints.Where(pnt => position.Field.GetAt(pnt) == MokuState.Empty);
        }

        private static IEnumerable<Point> GetNeighbourPoints(Point point, int size)
        {
            if (point.X > 0)
            {
                yield return point.Left();
            }
            if (point.Y > 0)
            {
                yield return point.Up();
            }
            if (point.X < size - 1)
            {
                yield return point.Right();
            }
            if (point.Y < size - 1)
            {
                yield return point.Down();
            }
        }

        /// <summary>
        /// Найти группы противника, соседствующие с данной группой
        /// </summary>
        /// <param name="position">позиция</param>
        /// <param name="grp">группа</param>
        /// <returns></returns>
        private static IEnumerable<Group> FindOppositeGroup(Position position, Group grp)
        {
            MokuState player = Opposite(grp.Player);
            var oppositePoints = grp.Points.SelectMany(point => GetNeighbourPoints(point, position.Size)).Where(point => position.Field.GetAt(point) == player);
            oppositePoints = oppositePoints.ToArray();
            var opposGroups = position._groups.Where(g => g.Points.Intersect(oppositePoints).Count() > 0);
            opposGroups = opposGroups.ToArray();
            return opposGroups;
        }

        private static MokuState Opposite(MokuState mokuState)
        {
            switch (mokuState)
            {
                case MokuState.Black:
                    return MokuState.White;
                case MokuState.White:
                    return MokuState.Black;
                default:
                    throw new ArgumentException("Invalid moku state for the stone");
            }
        }

        private Group UpdateGroups(Position position, Point point, MokuState player)
        {
            // скопировать группы, если исходная игровая или
            // создать группы, если исходная редактируемая
            if (IsEditable)
            {
                // создать группы на основе поля
                position._groups = MakeGroupsFromField(position);
                foreach (Group grp in position._groups)
                {
                    if (grp.Points.Contains(point))
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
                var groups = GetNearestGroup(point, player);

                Group group;
                switch (groups.Count)
                {
                case 0: // если их нет
                    // создаём новую группу
                    group = new Group(point, player);
                    position._groups.Add(group);
                    return group;
                case 1: // если одна группа
                    groups[0].Add(point);
                    return groups[0];
                default: // если больше одной группы
                    group = groups[0];
                    for (int i = 1 ; i < groups.Count; i++)
                    {
                        foreach (Point pnt in groups[i].Points)
                        {
                            group.Add(pnt);
                        }
                        _groups.Remove(groups[i]);
                    }
                    group.Add(point);
                    return group;
                }
            }
        }

        private List<Group> GetNearestGroup(Point point, MokuState player)
        {
            throw new NotImplementedException();
        }

        private List<Group> CopyGroups()
        {
            var collection = new List<Group>();
            foreach (var grp in _groups)
            {
                collection.Add(grp.Clone());
            }
            return collection;
        }

        private static List<Group> MakeGroupsFromField(Position position)
        {
            // создать коллекцию групп
            var collection = new List<Group>();

            // создать список камней
            var points = GeneratePoints(position.Field);

            // пока список камней не пуст
            while (points.Count > 0)
            {
                // создать группу из близлежащих камней
                Group item = position.ExtractGroup(points, position.Field);

                // добавить группу в коллекцию
                collection.Add(item);
            }

            // вернуть коллекцию
            return collection;
        }

        /// <summary>
        /// Выделить группу камней и удалить точки из списка
        /// </summary>
        /// <param name="points">точки, указывающие на камни</param>
        /// <param name="field">поле</param>
        /// <returns></returns>
        private Group ExtractGroup(List<Pair<Point, MokuState>> points, MokuField field)
        {
            var point = points[0];
            var group = new Group(point.First, point.Second);
            RecourseExtractGroup(group, point, points);
            return group;
        }

        private void RecourseExtractGroup(Group grp, Pair<Point, MokuState> point, List<Pair<Point, MokuState>> points)
        {
            var selectedNeighbour = points.Where(pair =>
            {
                if (point.Second != pair.Second)
                {
                    return false;
                }
                return point.First.Distance(pair.First) <= 1;
            }).ToArray();

            foreach (var pair in selectedNeighbour)
            {
                if (!grp.Points.Contains(pair.First))
                {
                    grp.Add(pair.First);
                }
                points.Remove(pair);
            }

            foreach (var pair in selectedNeighbour)
            {
                RecourseExtractGroup(grp, pair, points);
            }
        }

        /// <summary>
        /// Создать список точек, которые указывают на камни
        /// </summary>
        /// <param name="field"></param>
        /// <returns>Список точек, которые указывают на камни</returns>
        private static List<Pair<Point, MokuState>> GeneratePoints(MokuField field)
        {
            var points = new List<Pair<Point, MokuState>>();
            for (int x = 0; x < field.Size; x++)
            {
                for (int y = 0; y < field.Size; y++)
                {
                    var point = new Point(x, y);
                    MokuState mokuState = field.GetAt(point);
                    switch (mokuState)
                    {
                    case MokuState.Black:
                    case MokuState.White:
                        points.Add(new Pair<Point, MokuState>(point, mokuState));
                        break;
                    }
                }
            }
            return points;
        }
    }
}
