using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    public class OneDimensionCoordinates : ICoordinates
    {
        private readonly int _x;

        public OneDimensionCoordinates(int x)
        {
            _x = x;
        }

        public int NumberOfDimensions => 1;

        public int GetCoordinate(int dimension)
        {
            if (dimension == 0) return _x;
            throw new ArgumentException("invalid dimension");
        }

        public IEnumerable<ICoordinates> Neighbours(IPosition position)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ICoordinates other)
        {
            var o = other as OneDimensionCoordinates;
            if(o == null) throw new ArgumentException("should be OneDimensionCoordinates", "other");
            Contract.EndContractBlock();

            return GetCoordinate(0).CompareTo(o.GetCoordinate(0));
        }
    }
}