using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Valker.PlayOnLan.GoPlugin
{
    public class OneDimension : ICoordinates
    {
        private int _x;

        public OneDimension(int x)
        {
            _x = x;
        }

        public int NumberOfDimensions
        {
            get { return 1; }
        }

        public int GetCoordinate(int dimension)
        {
            if (dimension == 0) return _x;
            throw new ArgumentException("invalid dimension");
        }

        public IEnumerable<ICoordinates> Neighbours(IPosition position)
        {
            throw new System.NotImplementedException();
        }

        public int CompareTo(ICoordinates other)
        {
            var o = other as OneDimension;
            if(o == null) throw new ArgumentException("should be OneDimension", "other");
            Contract.EndContractBlock();

            return GetCoordinate(0).CompareTo(o.GetCoordinate(0));
        }
    }
}