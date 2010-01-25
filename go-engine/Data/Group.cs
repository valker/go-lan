using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public class Group
    {
        List<Point> _points = new List<Point>();
        public MokuState Player { get; private set; }

        public Group(Point point, MokuState player)
        {
            Player = player;
            _points.Add(point);
        }

        public Group(MokuState player)
        {
            Player = player;
        }

        public ICollection<Point> Points { get { return _points; } }

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