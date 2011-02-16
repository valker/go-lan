
namespace Valker.PlayServer
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using GapService;

    public class ConnectionEstablisher : IConnectionEstablisher
    {
        #region IConnectionEstablisher Members

        public event EventHandler<ConnectionEstablishedEventArgs> ConnectionEstablished;

        public int Port { get; private set; }

        public TcpListener TcpListener { get; set; }

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

        public void Stop()
        {
            TcpListener.Stop();
        }

        #endregion

        private void AcceptTcpClientCallback(IAsyncResult result)
        {
            TcpClient client = ExtractClient(result);

            if (client != null)
            {
                var e = new ConnectionEstablishedEventArgs {TcpClient = client};
                InvokeConnectionEstablished(e);
            }
        }

        private TcpClient ExtractClient(IAsyncResult result)
        {
            var asyncState = (TcpListener) result.AsyncState;
            TcpClient client = null;
            try
            {
                client = asyncState.EndAcceptTcpClient(result);
                BeginAcceptTcpClient();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
            }

            return client;
        }

        private void BeginAcceptTcpClient()
        {
            TcpListener.BeginAcceptTcpClient(AcceptTcpClientCallback, TcpListener);
        }

        private void InvokeConnectionEstablished(ConnectionEstablishedEventArgs e)
        {
            var connectionEstablished = ConnectionEstablished;
            if (connectionEstablished != null)
            {
                connectionEstablished(this, e);
            }
        }
    }
}