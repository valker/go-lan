using System;
using System.Net.Sockets;

namespace Valker.PlayServer
{
    public interface IConnectionEstablisher
    {
        int Port { get; }

        TcpListener TcpListener { get; }
        event EventHandler<ConnectionEstablishedEventArgs> ConnectionEstablished;

        void Start(int port);
    }
}