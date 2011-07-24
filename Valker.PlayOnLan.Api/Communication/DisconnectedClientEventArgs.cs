using System;

namespace Valker.PlayOnLan.Api.Communication
{
    /// <summary>
    /// Contains information about disconnected subject
    /// </summary>
    public class DisconnectedClientEventArgs : EventArgs
    {
        public object Identifier { get; set; }
    }
}