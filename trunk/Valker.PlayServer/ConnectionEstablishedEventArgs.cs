using System;
using System.Net.Sockets;

namespace Valker.PlayServer
{
    public class ConnectionEstablishedEventArgs : EventArgs
    {
        public TcpClient TcpClient { get; set; }
    }
}