using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.Api.Communication
{
    public interface IClientMessageExecuter
    {
        void UpdateSupportedGames(object sender, string[] games);
        void UpdatePartyStates(PartyState[] partyStates, IMessageConnector sender);
        void AcceptNewPlayer(bool status);
        void AcknowledgeRegistration(bool status, string parameters);
        void PartyBeginNotification(int partyId, string gameTypeId, string parameters);
    }
}