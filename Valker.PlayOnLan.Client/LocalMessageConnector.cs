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
            _parent = transport;
            _name = name;
        }

        protected string _name { get; set; }

        #region Implementation of IMessageConnector

        public void SendMessage(string message)
        {
            _parent.SendMessage(this, message);
        }

        public event EventHandler<MessageEventArgs> MessageArrived = delegate {  };

        #endregion

        public void OnMessageArrived(string message)
        {
            this.MessageArrived(this, new MessageEventArgs(message));
        }
    }
}