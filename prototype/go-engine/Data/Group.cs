using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

namespace go_engine.Data
{
    /// <summary>
    /// ���������� �����, ������� �������� ��������� ����� - ���������
    /// ������ ������������� ����� ������
    /// </summary>
    public class Group : ICollection<Point>
    {
        private List<Point> _points = new List<Point>();
        public MokuState Player { get; private set; }

        public Group(Point point, MokuState player)
        {
            Player = player;
            _points.Add(point);
        }

        public bool Equals(Group other)
        {
            return (Player == other.Player) && (_points.Except(other._points).Take(1).Count() == 0) &&
                   (other._points.Except(_points).Take(1).Count() == 0);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof (Group))
            {
                return false;
            }
            return Equals((Group) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public Group(Group first, Group second)
        {
            if (first.Player != second.Player)
            {
                throw new ArgumentException("Different players of parent group");
            }
            Player = first.Player;
            _points.AddRange(first);
            _points.AddRange(second);
        }

        private Group()
        {
        }

        public bool Remove(Point item)
        {
            throw new InvalidOperationException();
        }
        public Group RemovePoint(Point item)
        {
            Group grp = new Group();
            grp._points = new List<Point>(_points);
            if(!grp._points.Remove(item))
            {
                throw new ArgumentException("This point is not in this group.");
            }
            grp.Player = Player;
            return grp;
        }
        public int Count { get { return _points.Count; } }
        public bool IsReadOnly
        {
            get { return true; }
        }

        public void Add(Point point)
        {
            throw new InvalidOperationException();
        }

        public Group AddPoint(Point point)
        {
            if (_points.Contains(point))
            {
                throw new ArgumentException("This point is in this group.");
            }
            Group grp = new Group(point, Player);
            grp = new Group(this, grp);
            return grp;
        }

        public void Clear()
        {
            throw new InvalidOperationException();
        }

        public bool Contains(Point item)
        {
            return _points.Contains(item);
        }

        public void CopyTo(Point[] array, int arrayIndex)
        {
            _points.CopyTo(array, arrayIndex);
        }

        #region Implementation of IEnumerable

        public IEnumerator<Point> GetEnumerator()
        {
            return _points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}