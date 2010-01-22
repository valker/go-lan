using System;
using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public static class PointExtension {
        public static int Distance(this Point first, Point second)
        {
            return Math.Abs(first.X - second.X) + Math.Abs(first.Y - second.Y);
        }
    }
}