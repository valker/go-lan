using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace go_engine.Data
{
    /// <summary>
    /// Неизменный класс, который содержит коллекцию точек - координат
    /// камней принадлежащих одной группе
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
            Debug.Assert(Player != MokuState.Empty);
            Debug.Assert(Player != MokuState.None);
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