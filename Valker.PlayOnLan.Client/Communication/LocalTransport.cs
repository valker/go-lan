using System;

namespace Valker.PlayOnLan.Client.Communication
{
    public class LocalTransport : IDisposable
    {
        readonly LocalMessageConnector _serverConnector;

        public LocalMessageConnector ServerConnector
        {
            get { return _serverConnector; }
        }

        readonly LocalMessageConnector _clientConnector;

        public LocalMessageConnector ClientConnector
        {
            get { return _clientConnector; }
        }

        public LocalTransport()
        {
            _serverConnector = new LocalMessageConnector(this, "_server");
            _clientConnector = new LocalMessageConnector(this, "_client");
        }

        public void SendMessage(LocalMessageConnector sourceConnector, object fromIdentifier, object toIdentifier, string message)
        {
            if (ReferenceEquals(sourceConnector, ClientConnector))
            {
                ServerConnector.OnMessageArrived(fromIdentifier, toIdentifier, message);
            }
            else
            {
                ClientConnector.OnMessageArrived(fromIdentifier, toIdentifier, message);
            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            ClientConnector.DisposeImpl();
            ServerConnector.DisposeImpl();
        }

        #endregion

    }
}