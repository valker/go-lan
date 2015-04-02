using System;
using System.Collections.Generic;
using System.Linq;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Move : IMove
    {
        public Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Y { get; set; }
        public int X { get; set; }

        public Tuple<IPosition, IMoveInfo> Perform(IPosition currentPosition)
        {
            var coordinates = CreateCoordinates(X, Y);
            var state = currentPosition.GetStoneAt(coordinates);

            if (state is EmptyCell)
            {
                throw new GoException(ExceptionReason.Occuped);
            }
            // создать новую позицию, как копию исходной
            var position = (IPosition) (currentPosition.Clone());

            // поставить точку на поле
            position.ChangeCellState(coordinates, currentPosition.CurrentPlayer);

            // обновить группы для новой позиции
            var group = UpdateGroups(position, coordinates, currentPosition.CurrentPlayer);

            // удалить соседние группы, которые остались без дыханий
            var stoneCount = RemoveDeathOppositeGroups(position, group);

            // проверить живость новой группы
            if (!CheckIsLive(group, position))
            {
                throw new GoException(ExceptionReason.SelfDead);
            }

            return Tuple.Create<IPosition, IMoveInfo>(position, new MoveInfo(stoneCount));
        }

        private bool CheckIsLive(Group grp, IPosition position)
        {
            throw new NotImplementedException();
        }

        private int RemoveDeathOppositeGroups(IPosition position, Group grp)
        {
            // находим соседние группы противника
            IEnumerable<Group> oppositeGroups = FindOppositeGroup(position, grp);

            var toKill = oppositeGroups.Where(g => !CheckIsLive(g, position)).ToArray();

            int count = 0;

            // для всех групп из списка "на удаление"
            foreach (Group toKillGrp in toKill)
            {
                // добавляем количество камней в группе к количеству снятых с доски камней
                count += toKillGrp.Count;
                // убираем группу
                position.RemoveGroup(toKillGrp);
                // модифицируем поле
                foreach (var pnt in toKillGrp)
                {
                    position.ChangeCellState(pnt, new EmptyCell());
                    position.SetGroupAt(pnt, null);
                }
            }
            // возвращаем количество снятых камней
            return count;
        }

        private IEnumerable<Group> FindOppositeGroup(IPosition position, Group grp)
        {
            Stone[] players = Util.Opposite(grp.Player);
            List<Group> allGroups = new List<Group>();
            foreach (var player in players)
            {
                // координаты соседних точек, в которых находятся камни противника
                IEnumerable<ICoordinates> oppositePoints = grp.SelectMany(point => point.Neighbours(position)).Where(point => position.Field.GetAt(point) == player);
                oppositePoints = oppositePoints.ToArray();
                // группы противника, примыкающие к указанной точке
                var opposGroups = position._groups.Where(g => g.Intersect(oppositePoints).Count() > 0);
                allGroups.AddRange(opposGroups);
            }
            return allGroups;
        }

        private Group UpdateGroups(IPosition position, ICoordinates coordinates, IPlayer currentPlayer)
        {
            // получить соседние группы того же цвета
            List<Group> groups = position.GetNearestGroups(coordinates, currentPlayer);

            Group group;
            switch (groups.Count)
            {
                case 0: // если их нет
                    // создаём новую группу
                    group = new Group(coordinates, currentPlayer);
                    position.AddGroup(group);
                    position.SetGroupAt(coordinates, group);
                    return group;
                case 1: // если одна группа
                    Group grp = groups[0].AddPoint(coordinates);
                    foreach (var pnt in grp)
                    {
                        position.SetGroupAt(pnt, grp);
                    }
                    position.RemoveGroup(groups[0]);
                    position.AddGroup(grp);
                    return grp;
                default: // если больше одной группы
                    group = groups[0];
                    for (var i = 1; i < groups.Count; i++)
                    {
                        foreach (var pnt in groups[i])
                        {
                            group = group.AddPoint(pnt);
                        }
                    }
                    position.ExcludeGroups(groups);

                    group = group.AddPoint(coordinates);
                    position.AddGroup(group);
                    foreach (var pnt in group)
                    {
                        position.SetGroupAt(pnt, group);
                    }
                    return group;
            }
        }

        private static ICoordinates CreateCoordinates(int x, int y)
        {
            return new TwoDimensionsCoordinates(x, y);
        }
    }
}