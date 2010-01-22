using System;
using Microsoft.Xna.Framework;

namespace go_engine.Data
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
            return new Point(one.X, one.Y + 2);
        }
    }
}