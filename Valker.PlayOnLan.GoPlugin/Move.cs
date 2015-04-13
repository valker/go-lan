using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public class Move : IMove
    {
        public Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        private int Y { get; }
        private int X { get; }

        public IMoveConsequences Perform(IPosition currentPosition, IPlayerProvider playerProvider)
        {
            ICoordinates coordinates = Util.CreateCoordinates(X, Y);
            return currentPosition.MoveConsequences(coordinates);
        }
    }
}