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

        public void SendMessage(string message)
        {
            this._parent.SendMessage(this, message);
        }

        public event EventHandler<MessageEventArgs> MessageArrived = delegate { };
        public string Name { get; set; }

        #endregion

        public void OnMessageArrived(string message)
        {
            this.MessageArrived(this, new MessageEventArgs(message));
        }
    }
}