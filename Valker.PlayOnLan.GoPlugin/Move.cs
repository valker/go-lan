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

        private int Y { get; }
        private int X { get; }

        public Tuple<IPosition, IMoveInfo> Perform(IPosition currentPosition, IPlayerProvider playerProvider)
        {
            var coordinates = CreateCoordinates(X, Y);
            var state = currentPosition.GetCellAt(coordinates);

            if (!(state is EmptyCell))
            {
                throw new GoException(ExceptionReason.Occuped);
            }
            // ������� ����� �������, ��� ����� ��������
            var position = (IPosition) (currentPosition.Clone());

            // ��������� ����� �� ����
            position.ChangeCellState(coordinates, new PlayerCell(currentPosition.CurrentPlayer));

            // �������� ������ ��� ����� �������
            var group = UpdateGroups(position, coordinates, currentPosition.CurrentPlayer);

            // ������� �������� ������, ������� �������� ��� �������
            var stoneCount = RemoveDeathOppositeGroups(position, @group, playerProvider);

            // ��������� ������� ����� ������
            if (!CheckIsLive(group, position))
            {
                throw new GoException(ExceptionReason.SelfDead);
            }

            return Tuple.Create<IPosition, IMoveInfo>(position, new MoveInfo(stoneCount));
        }

        private static bool CheckIsLive(Group grp, IPosition position)
        {
            var dame = GetDame(grp, position).ToArray();
            return dame.Take(1).Any();
        }

        private static IEnumerable<ICoordinates> GetDame(Group grp, IPosition position)
        {
            return grp.SelectMany(delegate(ICoordinates point)
            {
                var coordinateses = GetDame(point, position).ToArray();
                return coordinateses;
            });
        }

        private static IEnumerable<ICoordinates> GetDame(ICoordinates point, IPosition position)
        {
            IEnumerable<ICoordinates> coordinateses = point.Neighbours(position).ToArray();
            return coordinateses.Where(pnt => position.GetCellAt(pnt) is EmptyCell);
        }

        private int RemoveDeathOppositeGroups(IPosition position, Group grp, IPlayerProvider playerProvider)
        {
            // ������� �������� ������ ����������
            IEnumerable<Group> oppositeGroups = FindOppositeGroup(position, grp, playerProvider);

            var groupsToKill = oppositeGroups.Where(g => !CheckIsLive(g, position)).ToArray();

            int count = 0;

            // ��� ���� ����� �� ������ "�� ��������"
            foreach (Group groupToKill in groupsToKill)
            {
                // ��������� ���������� ������ � ������ � ���������� ������ � ����� ������
                count += groupToKill.Count;
                // ������� ������
                position.RemoveGroup(groupToKill);
                // ������������ ����
                foreach (var pnt in groupToKill)
                {
                    position.ChangeCellState(pnt, new EmptyCell());
                    position.SetGroupAt(pnt, null);
                }
            }
            // ���������� ���������� ������ ������
            return count;
        }

        private static IPlayer[] Opposite(IPlayer player, IPlayerProvider playerProvider)
        {
            return playerProvider.GetPlayers().Except(new[] {player}).ToArray();
        }


        private IEnumerable<Group> FindOppositeGroup(IPosition position, Group grp, IPlayerProvider playerProvider)
        {
            var players = Opposite(grp.Player, playerProvider);
            List<Group> allGroups = new List<Group>();
            foreach (var player in players)
            {
                // ���������� �������� �����, � ������� ��������� ����� ����������
                IEnumerable<ICoordinates> oppositePoints = grp.SelectMany(point => point.Neighbours(position)).Where(point => position.GetCellAt(point).Equals(new PlayerCell(player)));
                oppositePoints = oppositePoints.ToArray();
                // ������ ����������, ����������� � ��������� �����
                IEnumerable<Group> opposGroups = position.Groups.Where(g => g.Intersect(oppositePoints).Any());
                allGroups.AddRange(opposGroups);
            }
            return allGroups;
        }

        private Group UpdateGroups(IPosition position, ICoordinates coordinates, IPlayer currentPlayer)
        {
            // �������� �������� ������ ���� �� �����
            List<Group> groups = position.GetNearestGroups(coordinates, currentPlayer);

            Group group;
            switch (groups.Count)
            {
                case 0: // ���� �� ���
                    // ������ ����� ������
                    group = new Group(coordinates, currentPlayer);
                    position.AddGroup(group);
                    position.SetGroupAt(coordinates, group);
                    return group;
                case 1: // ���� ���� ������
                    Group grp = groups[0].AddPoint(coordinates);
                    foreach (var pnt in grp)
                    {
                        position.SetGroupAt(pnt, grp);
                    }
                    position.RemoveGroup(groups[0]);
                    position.AddGroup(grp);
                    return grp;
                default: // ���� ������ ����� ������
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

    public class PlayerCell : ICell
    {
        protected bool Equals(PlayerCell other)
        {
            return Equals(Player, other.Player);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlayerCell) obj);
        }

        public override int GetHashCode()
        {
            return (Player != null ? Player.GetHashCode() : 0);
        }

        public PlayerCell(IPlayer player)
        {
            Player = player;
        }

        public override string ToString()
        {
            return string.Format("PLAYER:{0}", Player.PlayerName);
        }

        public IPlayer Player { get; set; }
    }
}