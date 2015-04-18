using System;

namespace Valker.PlayOnLan.XmppTransport
{
    class XmppTransportException : Exception
    {
        public XmppTransportException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}