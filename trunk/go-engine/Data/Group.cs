using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    internal class Group
    {
        List<Point> _points = new List<Point>();
        public MokuState Player { get; private set; }

        public Group(Point point, MokuState player)
        {
            Player = player;
            _points.Add(point);
        }

        private Group()
        {
        }

        public IEnumerable<Point> Points { get { return _points; } }
        public Group Clone()
        {
            var group = new Group();
            group._points = new List<Point>();
            group._points.Capacity = _points.Count;
            foreach (var point in _points)
            {
                group._points.Add(point);
            }
            group.Player = Player;
            return group;
        }

        public int Count { get { return _points.Count; } }

        public void Add(Point point)
        {
            if (_points.Contains(point))
            {
                throw new ArgumentException("This point is also exist in group");
            }
            _points.Add(point);
        }
    }
}