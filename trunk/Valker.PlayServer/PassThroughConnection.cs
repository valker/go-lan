using System;

namespace Valker.PlayServer
{
    internal class PassThroughConnection : IPassThroughConnection
    {
        private bool _closed;

        public PassThroughConnection(IIdentifiedClient announcer, IClient acquirer)
        {
            Announcer = announcer;
            Acquirer = acquirer;
        }

        #region IPassThroughConnection Members

        public event EventHandler Closed;

        public void Start()
        {
            StartClient(Acquirer);
            StartClient(Announcer.Client);
        }

        public IClient Acquirer { get; set; }

        public IIdentifiedClient Announcer { get; set; }

        #endregion

        private void CloseClient(IClient client)
        {
            client.Dispose();
            client.ReadCompleted -= Passthrough;
        }

        private void Disconnected(object sender, EventArgs e)
        {
            var client = (IClient) sender;
            client.Disconnected -= Disconnected;
            bool flag = false;
            lock (this)
            {
                if (!_closed)
                {
                    _closed = true;
                    flag = true;
                }
            }
            if (flag)
            {
                CloseClient(client);
                client = ReferenceEquals(client, Acquirer) ? Announcer.Client : Acquirer;
                CloseClient(client);
                InvokeClosed(e);
            }
        }

        private void InvokeClosed(EventArgs e)
        {
            EventHandler closed;
            lock (this)
            {
                closed = Closed;
            }
            if (closed != null)
            {
                closed(this, e);
            }
        }

        private void Passthrough(object sender, ReadCompletedEventArgs args)
        {
            var client = (IClient) sender;
//            bool flag = client.ClientType == ClientType.Announcer;
//            IClient client2 = flag ? Announcer.Client : Acquirer;
//            IClient client3 = flag ? Acquirer : Announcer.Client;
//            int bytesRead = args.BytesRead;
//            var destinationArray = new byte[bytesRead];
//            Array.Copy(args.Buffer, destinationArray, bytesRead);
//            client3.WriteAsync(destinationArray);
//            client2.ReadAsync();
        }

        private void StartClient(IClient client)
        {
            client.ReadCompleted += Passthrough;
            client.Disconnected += Disconnected;
            client.ReadAsync();
        }
    }
}