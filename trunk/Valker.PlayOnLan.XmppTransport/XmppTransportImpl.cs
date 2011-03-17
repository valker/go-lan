using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.XmppTransport
{
    public class XmppTransportImpl : IMessageConnector
    {
        public XmppTransportImpl(string name)
        {
            Name = name;
        }

        #region IMessageConnector Members

        public string Name { get; set; }

        #endregion

        #region Implementation of IMessageConnector

        public void Send(string message)
        {
            // TODO: implement send message
        }

        public string ConnectorName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void Send(object fromIdentifier, object toIdentifier, string message)
        {
            throw new NotImplementedException();
        }

        public string[] Clients
        {
            get { throw new NotImplementedException(); }
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
}