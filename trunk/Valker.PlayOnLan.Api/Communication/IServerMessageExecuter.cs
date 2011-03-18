namespace Valker.PlayOnLan.Api.Communication
{
    public interface IServerMessageExecuter
    {
        void RetrieveSupportedGames(IAgentInfo sender);
        void RegisterNewParty(IAgentInfo agent, string gameId, string parameters);
        void UpdatePartyStates(IAgentInfo agent);
        void AcceptPartyRequest(int partyId, string accepterName);
        void RegisterNewPlayer(IAgentInfo agent, string name);
        void ExecuteServerGameMessage(IAgentInfo sender, string text, int id);
    }
}