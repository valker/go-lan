using System;

namespace Valker.PlayOnLan.Api.Game
{
    /// <summary>
    /// Define an interface for specific game server
    /// </summary>
    public interface IGameServer
    {
        /// <summary>
        /// Process a message from given player
        /// </summary>
        /// <param name="sender">player, that sent this message</param>
        /// <param name="message">message text</param>
        void ProcessMessage(IPlayer sender, string message);

        /// <summary>
        /// Server raise this event when it wants to send a message
        /// </summary>
        event EventHandler<OnMessageEventArgs> OnMessageReady;

        /// <summary>
        /// Called when server should start the game
        /// </summary>
        void Start();
    }
}
