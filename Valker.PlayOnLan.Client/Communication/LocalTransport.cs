using System;

namespace Valker.PlayOnLan.Client.Communication
{
    public class LocalTransport : IDisposable
    {
        public LocalMessageConnector ServerConnector { get; }

        public LocalMessageConnector ClientConnector { get; }

        public LocalTransport()
        {
            ServerConnector = new LocalMessageConnector(this, "_server");
            ClientConnector = new LocalMessageConnector(this, "_client");
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