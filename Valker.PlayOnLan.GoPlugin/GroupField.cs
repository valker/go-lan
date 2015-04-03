using System;
using System.Collections.Generic;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    internal class GroupField
    {
        private readonly Dictionary<ICoordinates, Group> _field; 

        public GroupField(GroupField parent)
        {
            _field = new Dictionary<ICoordinates, Group>(parent._field);
        }

        public GroupField(int size)
        {
            _field = new Dictionary<ICoordinates, Group>();
        }

        public void SetAt(ICoordinates point, Group grp)
        {
            _field[point] = grp;
        }

        public Group GetAt(ICoordinates point)
        {
            return _field[point];
        }
    }
}