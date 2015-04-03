using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IPlayerProvider
    {
        IPlayer[] GetPlayers();
        IPlayer GetFirstPlayer();
        IPlayer GetNextPlayer(IPlayer player);
    }
}