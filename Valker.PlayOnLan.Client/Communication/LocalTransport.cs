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


        public void SendMessage(LocalMessageConnector From, string message, object To)
        {
            if (ReferenceEquals(From, ClientConnector))
            {
                ServerConnector.OnMessageArrived(message, To);
            }
            else
            {
                ClientConnector.OnMessageArrived(message, To);
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