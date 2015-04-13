using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Move : IMove
    {
        public Move(ICoordinates coordinates)
        {
            Coordinates = coordinates;
        }

        private ICoordinates Coordinates { get; }


        public IMoveConsequences Perform(IPosition currentPosition)
        {
            return currentPosition.MoveConsequences(Coordinates);
        }
    }
}