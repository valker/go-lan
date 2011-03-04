namespace Valker.PlayOnLan.Api.Communication
{
    public interface IServerMessageExecuter
    {
        string[] RetrieveSupportedGames();
        void Send(IClientInfo recepient, string message);
        PartyStatus RegisterNewParty(string name, string gameId, IClientInfo client);
        void UpdatePartyStates();

        void AcceptPartyRequest(string RequesterName, string GameType, string AccepterName);

        void RegisterNewPlayer(string Name, IClientInfo connector);
    }
}