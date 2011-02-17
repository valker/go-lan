using System;
using System.Diagnostics;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.XmppTransport
{
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

        public void SendMessage(string message)
        {
            Trace.WriteLine("TODO: implement send message");
        }

        public event EventHandler<MessageEventArgs> MessageArrived;

        #endregion
    }
}