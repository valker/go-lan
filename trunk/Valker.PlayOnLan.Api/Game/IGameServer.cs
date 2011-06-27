using System;

namespace Valker.PlayOnLan.Api.Game
{
    public interface IGameServer
    {
        void ProcessMessage(IPlayer sender, string message);

        /// <summary>
        /// Server raise this event when it wants to send a message
        /// </summary>
        event EventHandler<OnMessageEventArgs> OnMessage;

        /// <summary>
        /// Called when server should start the game
        /// </summary>
        void Start();
    }
}
