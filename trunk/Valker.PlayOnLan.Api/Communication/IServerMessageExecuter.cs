namespace Valker.PlayOnLan.Api.Communication
{
    public interface IServerMessageExecuter
    {
        void RetrieveSupportedGames(IClientInfo sender);
        void RegisterNewParty(IClientInfo client, string gameId, string parameters);
        void UpdatePartyStates(IClientInfo client);
        void AcceptPartyRequest(int partyId, string accepterName);
        void RegisterNewPlayer(IClientInfo client, string name);
        void ExecuteServerGameMessage(IClientInfo sender, string text, object fromIdentifier, int id);
    }
}