using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.XmppTransport
{
    public class XmppTransportImpl : IMessageConnector
    {
        #region Implementation of IMessageConnector

        public void SendMessage(string message)
        {
            //throw new NotImplementedException();
        }

        public event EventHandler<MessageEventArgs> MessageArrived;

        #endregion
    }
}
