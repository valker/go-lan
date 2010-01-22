using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public class Position
    {
        public MokuField Field { get; private set; }
        private GroupCollection _groups = new GroupCollection();

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
        /// <param name="Rules">особенности правил</param>
        /// <returns>новая позиция и число съеденных камней</returns>
        internal Pair<Position, int> Move(Point point, MokuState player, Rules Rules)
        {
            var position = new Position(Size);
            position.Field = new MokuField(Field);
            position._groups = IsEditable ? MakeGroupsFromField() : CopyGroups();
            throw new NotImplementedException();
        }

        private GroupCollection CopyGroups()
        {
            var collection = new GroupCollection();
            foreach (var grp in _groups)
            {
                collection.Add(grp.Clone());
            }
            return collection;
        }

        private GroupCollection MakeGroupsFromField()
        {
            // создать коллекцию групп
            var collection = new GroupCollection();

            // создать список камней
            var points = GeneratePoints(Field);

            // пока список камней не пуст
            while (points.Count > 0)
            {
                // создать группу из близлежащих камней
                Group item = ExtractGroup(points, Field);

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
            });

            foreach (var pair in selectedNeighbour)
            {
                grp.Add(pair.First);
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
