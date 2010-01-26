﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    /// <summary>
    /// Класс задаёт конкретную позицию камней, возникшую в игре, или при редактировании поля
    /// </summary>
    public class Position : IPosition
    {
        /// <summary>
        /// Расположение камней
        /// </summary>
        public MokuField Field { get; private set; }

        /// <summary>
        /// Создаём позицию с заданным размером
        /// </summary>
        /// <param name="size"></param>
        private Position(int size)
        {
            // создаём новое поле для камней и инициализируем его
            Field = new MokuField(size);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Field.SetAt(new Point(x,y), MokuState.Empty);
                }
            }

            // создаём новое поле для ссылок на группы (инициализируется нулями
            _groupField = new GroupField(size);
        }

        public bool IsEditable { get; private set; }

        public static IPosition CreateInitial(int size)
        {
            Position position = new Position(size);
            position.IsEditable = true;
            return position;
        }

        public IPosition CopyMokuField()
        {
            Position position = new Position(Field.Size);
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
        public Pair<IPosition, int> Move(Point point, MokuState player)
        {
            var state = Field.GetAt(point);
            if (state != MokuState.Empty)
            {
                throw new GoException(ExceptionReason.Occupped);
            }
            // создать новую позицию, как копию исходной
            var position = new Position(Field.Size);
            position.Field = new MokuField(Field);
            position._groupField = new GroupField(_groupField);

            // поставить точку на поле
            position.Field.SetAt(point, player);

            // обновить группы для новой позиции
            var group = UpdateGroups(position, point, player, IsEditable);

            // удалить соседние группы, которые остались без дыханий
            int stoneCount = RemoveDeathOppositeGroups(position, group);

            return new Pair<IPosition, int>(position, stoneCount);
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
                foreach (var pnt in toKillGrp)
                {
                    position.Field.SetAt(pnt, MokuState.Empty);
                    position._groupField.SetAt(pnt, null);
                }
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
            return grp.SelectMany(point => GetDame(point, position));
        }

        private static IEnumerable<Point> GetDame(Point point, Position position)
        {
            return point.Neighbours(position.Field.Size).Where(pnt => position.Field.GetAt(pnt) == MokuState.Empty);
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
            var oppositePoints = grp.SelectMany(point => point.Neighbours(position.Field.Size)).Where(point => position.Field.GetAt(point) == player);
            oppositePoints = oppositePoints.ToArray();
            var opposGroups = position._groups.Where(g => g.Intersect(oppositePoints).Count() > 0);
            opposGroups = opposGroups.ToArray();
            return opposGroups;
        }

        public static MokuState Opposite(MokuState mokuState)
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

        /// <summary>
        /// Обновить данные по группам для нового состояния поля
        /// </summary>
        /// <param name="position"></param>
        /// <param name="point"></param>
        /// <param name="player"></param>
        /// <param name="isParentEditable"></param>
        /// <returns></returns>
        private Group UpdateGroups(Position position, Point point, MokuState player, bool isParentEditable)
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
                    groups[0] = groups[0].AddPoint(point);
                    position._groupField.SetAt(point, groups[0]);
                    return groups[0];
                default: // если больше одной группы
                    group = groups[0];
                    for (int i = 1 ; i < groups.Count; i++)
                    {
                        foreach (Point pnt in groups[i])
                        {
                            group = group.AddPoint(pnt);
                        }
                    }
                    groups.RemoveRange(1, groups.Count - 1);
                    
                    group = group.AddPoint(point);
                    groups[0] = group;
                    foreach (var pnt in group)
                    {
                        position._groupField.SetAt(pnt, group);
                    }
                    return group;
                }
            }
        }

        private List<Group> GetNearestGroups(Point point, MokuState player)
        {
            IEnumerable<Point> neighbours = point.Neighbours(Field.Size);
            List<Group> groups =
                neighbours.Select(point1 => _groupField.GetAt(point1)).Where(
                    grp => grp != null && grp.Player == player).Distinct().ToList();
            return groups;
        }

        private List<Group> CopyGroups()
        {
            var collection = new List<Group>(_groups);
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
                if (!grp.Contains(pair.First))
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

//        public static Group Clone(Group g)
//        {
//            var group = new Group(g.Player);
//            foreach (var point in g.Points)
//            {
//                group.Points.Add(point);
//            }
//            return group;
//        }

        private List<Group> _groups = new List<Group>();
        private GroupField _groupField;
    }

    public class GoException : Exception
    {
        public GoException(ExceptionReason reason)
        {
            Reason = reason;
        }

        public ExceptionReason Reason { get; set; }
    }
    public enum ExceptionReason
    {
        Occupped
    }
}