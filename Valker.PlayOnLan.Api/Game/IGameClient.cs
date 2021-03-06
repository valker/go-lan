﻿using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Api.Game
{
    /// <summary>
    /// Defines an interface of client part of game plugin
    /// </summary>
    public interface IGameClient
    {
        /// <summary>
        /// Create the form for playing
        /// </summary>
        /// <param playerName="parameters">parameters of the party</param>
        /// <param name="parameters"></param>
        /// <param name="playerName"></param>
        /// <param name="gui"></param>
        /// <returns>Form to be shown</returns>
        IPlayingForm CreatePlayingForm(string parameters, string playerName, string gui);

        /// <summary>
        /// New message for this client appeared
        /// </summary>
        /// <param name="message"></param>
        void ExecuteMessage(string message);

        /// <summary>
        /// Raised when the client wants to be closed
        /// </summary>
        event EventHandler Closed;

        event EventHandler<MessageEventArgs> OnMessageReady;
    }
}