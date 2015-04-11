using System.Linq;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    static class Util
    {
        public static IPlayer[] Opposite(IPlayer player, IPlayerProvider playerProvider)
        {
            return playerProvider.GetPlayers().Except(new[] { player }).ToArray();
        }

        public static ICoordinates CreateCoordinates(int x, int y)
        {
            return new TwoDimensionsCoordinates(x, y);
        }
    }
}
