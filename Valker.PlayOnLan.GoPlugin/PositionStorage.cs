using System.Collections.Generic;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    public class PositionStorage : IPositionStorage
    {
        readonly Dictionary<IPosition, IPosition> _childToParentDirectory = new Dictionary<IPosition, IPosition>(); 

        public PositionStorage(int size, IPlayerProvider playerProvider, ICoordinatesFactory coordinatesFactory) : this(Position.CreateInitial(size, playerProvider, coordinatesFactory))
        {
        }

        public PositionStorage(IPosition initial)
        {
            Initial = initial;
        }

        /// <summary>
        /// Возвращает исходную позицию в дереве игры
        /// </summary>
        public IPosition Initial { get; }

        public void AddChildPosition(IPosition parent, IPosition child)
        {
            _childToParentDirectory.Add(child, parent);
        }

        public int ExistParent(IPosition knownChild, IPosition possibleParent)
        {
            int count = 0;
            while (true)
            {
                IPosition value;
                if (!_childToParentDirectory.TryGetValue(knownChild, out value))
                {
                    return -1;
                }
                ++count;
                if (possibleParent.Equals(value))
                {
                    return count;
                }
                knownChild = value;
            }
        }
    }
}
