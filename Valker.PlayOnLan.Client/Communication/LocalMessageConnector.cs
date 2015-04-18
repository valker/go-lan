using System;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Client.Communication;

namespace Valker.PlayOnLan.Client
{
    public class LocalMessageConnector : IMessageConnector
    {
        private readonly LocalTransport _parent;

        public LocalMessageConnector(LocalTransport transport, string name)
        {
            _parent = transport;
            ConnectorName = name;
        }

        #region Implementation of IMessageConnector

        public event EventHandler<MessageEventArgs> MessageArrived = delegate { };
        public event EventHandler Closed = delegate { };
        public string ConnectorName { get; set; }

        #endregion

        public void OnMessageArrived(object fromIdentifier, object toIdentifier, string message)
        {
            MessageArrived(this, new MessageEventArgs(fromIdentifier, toIdentifier, message));
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _parent.Dispose();
        }

        #endregion

        public void DisposeImpl()
        {
            MessageArrived = null;
            Closed(this, EventArgs.Empty);
            Closed = null;
        }

        #region IMessageConnector Members

        #endregion

        #region IMessageConnector Members


        public void Send(object fromIdentifier, object toIdentifier, string message)
        {
            _parent.SendMessage(this, fromIdentifier, toIdentifier, message);
        }

        public void WatchOther(object identifier)
        {
            // todo: to be implemented
        }

        public event EventHandler<DisconnectedClientEventArgs> DisconnectedOther;

        #endregion
    }
}