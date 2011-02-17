using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.XmppTransport
{
    public class XmppTransportImpl : IMessageConnector
    {
        public XmppTransportImpl(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        #region Implementation of IMessageConnector

        public void SendMessage(string message)
        {
            Trace.WriteLine("TODO: implement send message");
        }

        public event EventHandler<MessageEventArgs> MessageArrived;

        #endregion
    }
}
