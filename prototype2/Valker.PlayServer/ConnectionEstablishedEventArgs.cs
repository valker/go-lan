
namespace Valker.PlayServer
{
    using System;
    using System.Net.Sockets;

    /// <summary>
    /// Contains information about connected client
    /// </summary>
    public class ConnectionEstablishedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the instance of connected client
        /// </summary>
        public TcpClient TcpClient { get; set; }
    }
}