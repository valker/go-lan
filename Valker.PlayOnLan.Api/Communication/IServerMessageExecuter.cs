using Valker.PlayOnLan.Api.Game;
namespace Valker.PlayOnLan.Api.Communication
{
    public interface IServerMessageExecuter
    {
        /// <summary>
        /// Send the message to receipient
        /// </summary>
        /// <param name="recepient"></param>
        /// <param name="message"></param>
        void Send(IClientInfo recepient, string message);

        string[] RetrieveSupportedGames();
        PartyStatus RegisterNewParty(IClientInfo client, string gameId, string parameters);
        void UpdatePartyStates(IClientInfo client);
        void AcceptPartyRequest(int partyId, string accepterName);
        void RegisterNewPlayer(IClientInfo client, string name);
    }
}