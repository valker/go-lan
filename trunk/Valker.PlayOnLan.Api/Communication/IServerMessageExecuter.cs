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
        PartyStatus RegisterNewParty(IClientInfo client, string name, string gameId);
        void UpdatePartyStates();
        void AcceptPartyRequest(string RequesterName, string GameType, string AccepterName);
        void RegisterNewPlayer(IClientInfo client, string Name);
    }
}