using System;
using System.Collections.Generic;

namespace Valker.PlayOnLan.Utilities
{
    public static class PointExtension
    {
        public static int Distance(this Point first, Point second)
        {
            return Math.Abs(first.X - second.X) + Math.Abs(first.Y - second.Y);
        }

        public static Point Left(this Point one)
        {
            return new Point(one.X - 1, one.Y);
        }

        public static Point Right(this Point one)
        {
            return new Point(one.X + 1, one.Y);
        }

        public static Point Up(this Point one)
        {
            return new Point(one.X, one.Y - 1);
        }

        public static Point Down(this Point one)
        {
            return new Point(one.X, one.Y + 1);
        }

        public static IEnumerable<Point> Neighbours(this Point point, int size)
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
    }
}