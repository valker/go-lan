namespace Valker.PlayOnLan.Api.Communication
{
    public interface IServerMessageExecuter
    {
        string[] RetrieveSupportedGames();
        void Send(string message);
        PartyStatus RegisterNewParty(string name, string gameId);
    }
}