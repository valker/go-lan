namespace Valker.PlayOnLan.Api.Communication
{
    public interface IClientMessageExecuter
    {
        void UpdateSupportedGames(object sender, string[] games);
        void UpdatePartyStates(PartyState[] partyStates, IMessageConnector sender);

        void AcceptNewPlayer(bool status);

        void AcknowledgeRegistration(bool Status);
    }
}