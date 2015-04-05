namespace Valker.PlayOnLan.Api.Game
{
    public interface IPlayerProvider
    {
        IPlayer[] GetPlayers();
        IPlayer GetFirstPlayer();
        IPlayer GetNextPlayer(IPlayer player);
        IPlayer GetMe();
    }
}