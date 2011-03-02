using System;
using System.Collections.Generic;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Client.Communication
{
    public class LocalTransport : IDisposable
    {
        LocalMessageConnector _serverConnector;

        public LocalMessageConnector ServerConnector
        {
            get { return _serverConnector; }
        }

        LocalMessageConnector _clientConnector;

        public LocalMessageConnector ClientConnector
        {
            get { return _clientConnector; }
        }

        public LocalTransport()
        {
            _serverConnector = new LocalMessageConnector(this, "server");
            _clientConnector = new LocalMessageConnector(this, "client");
        }


        public void SendMessage(LocalMessageConnector connector, string message)
        {
            if (ReferenceEquals(connector, ClientConnector))
            {
                ServerConnector.OnMessageArrived(message);
            }
            else
            {
                ClientConnector.OnMessageArrived(message);
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