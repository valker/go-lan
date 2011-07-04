using System;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    internal class GroupField
    {
        private readonly Group[,] _field;

        public GroupField(GroupField parent)
        {
            _field = (Group[,]) parent._field.Clone();
        }

        public GroupField(int size)
        {
            _field = new Group[size,size];
        }

        public void SetAt(Point point, Group grp)
        {
            _field[point.X, point.Y] = grp;
        }

        public Group GetAt(Point point)
        {
            return _field[point.X, point.Y];
        }
    }
}