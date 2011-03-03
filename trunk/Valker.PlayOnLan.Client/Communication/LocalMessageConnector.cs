using System;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Client.Communication;

namespace Valker.PlayOnLan.Client
{
    public class LocalMessageConnector : IMessageConnector
    {
        private LocalTransport _parent;

        public LocalMessageConnector(LocalTransport transport, string name)
        {
            this._parent = transport;
            this.Name = name;
        }

        #region Implementation of IMessageConnector

        public event EventHandler<MessageEventArgs> MessageArrived = delegate { };
        public event EventHandler Closed = delegate { };
        public string Name { get; set; }

        #endregion

        public void OnMessageArrived(string message, string To)
        {
            this.MessageArrived(this, new MessageEventArgs(message, To));
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


        public string[] Clients
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IMessageConnector Members


        public void Send(string To, string message)
        {
            _parent.SendMessage(this, message, To);
        }

        #endregion
    }
}