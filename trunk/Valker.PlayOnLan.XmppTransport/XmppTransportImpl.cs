using System;
using System.Diagnostics;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.XmppTransport
{
#if false
    public class XmppTransportImpl : IMessageConnector
    {
        public XmppTransportImpl(string name)
        {
            this.Name = name;
        }

        #region IMessageConnector Members

        public string Name { get; set; }

        #endregion

        #region Implementation of IMessageConnector

        public void Send(string message)
        {
            // TODO: implement send message
        }

        public event EventHandler<MessageEventArgs> MessageArrived;
        public event EventHandler Closed;

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            // TODO: to be implemented
            return;
        }

        #endregion
    }
#endif
}