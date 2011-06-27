using System;
using System.Collections.Generic;

namespace Valker.PlayOnLan.Api.Game
{
    /// <summary>
    /// Contains information about message to be sent
    /// </summary>
    public sealed class OnMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Gets/sets receipients of the message
        /// </summary>
        public IEnumerable<IPlayerBase> Receipients { get; set; }

        /// <summary>
        /// Gets/sets the message body
        /// </summary>
        public string Message { get; set; }
    }
}