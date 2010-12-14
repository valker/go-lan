using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using GapService;

namespace Valker.PlayServer
{
    public class ConnectionEstablisher : IConnectionEstablisher
    {
        #region IConnectionEstablisher Members

        public event EventHandler<ConnectionEstablishedEventArgs> ConnectionEstablished;

        public void Start(int port)
        {
            Port = port;
            try
            {
                TcpListener = new TcpListener(IPAddress.Any, Port);
                TcpListener.Start();
                BeginAcceptTcpClient();
            }
            catch (SocketException exception)
            {
                TcpListener.Stop();
                throw new GapException("Cannot start listener", exception);
            }
            catch (ObjectDisposedException exception2)
            {
                TcpListener.Stop();
                throw new GapException("Socket has been closed", exception2);
            }
        }

        public int Port { get; set; }

        public TcpListener TcpListener { get; set; }

        #endregion

        private void AcceptTcpClientCallback(IAsyncResult result)
        {
            var asyncState = (TcpListener) result.AsyncState;
            try
            {
                TcpClient client = asyncState.EndAcceptTcpClient(result);
                var e = new ConnectionEstablishedEventArgs();
                e.TcpClient = client;
                InvokeConnectionEstablished(e);
                BeginAcceptTcpClient();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
            }
        }

        private void BeginAcceptTcpClient()
        {
            TcpListener.BeginAcceptTcpClient(AcceptTcpClientCallback, TcpListener);
        }

        private void InvokeConnectionEstablished(ConnectionEstablishedEventArgs e)
        {
            EventHandler<ConnectionEstablishedEventArgs> connectionEstablished;
            lock (this)
            {
                connectionEstablished = ConnectionEstablished;
            }
            if (connectionEstablished != null)
            {
                connectionEstablished(this, e);
            }
        }
    }
}