using System;

namespace Valker.PlayOnLan.Api.Communication
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(object fromIdentifier, object toIdentifier, string message)
        {
            Message = message;
            FromIdentifier = fromIdentifier;
            ToIdentifier = toIdentifier;
        }

        public string Message { get; set; }

        public object FromIdentifier { get; set; }

        public object ToIdentifier { get; set; }
    }
}