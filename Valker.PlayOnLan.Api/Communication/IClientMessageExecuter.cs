using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.Api.Communication
{
    /// <summary>
    /// Defines an interface of the client
    /// </summary>
    public interface IClientMessageExecuter
    {
        /// <summary>
        /// Server notifies about supported games
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="games"></param>
        void UpdateSupportedGames(IMessageConnector sender, string[] games);

        /// <summary>
        /// Server notifies about states of parties
        /// </summary>
        /// <param name="partyStates"></param>
        /// <param name="sender"></param>
        void UpdatePartyStates(IMessageConnector sender, PartyState[] partyStates);

        /// <summary>
        /// Server notifies about the status of registration new player
        /// </summary>
        /// <param name="status">true if player is registred OK</param>
        void AcceptNewPlayer(bool status);

        /// <summary>
        /// Server notifies about start of new party
        /// </summary>
        /// <param name="sender">which connector throw this message</param>
        /// <param name="partyId">unique identifier of the party across one server</param>
        /// <param name="gameTypeId">type of the game</param>
        /// <param name="parameters">parameters of the game</param>
        /// <param name="playerProvider"></param>
        void PartyBeginNotification(IMessageConnector sender, int partyId, string gameTypeId, string parameters, string[] players, string thisPlayer);

        /// <summary>
        /// Server notifies about new game event
        /// </summary>
        /// <param name="sender">which connector throw this message</param>
        /// <param name="message">message from game server</param>
        void ExecuteGameMessage(IMessageConnector sender, string message);
    }
}