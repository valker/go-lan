using System;

namespace Valker.PlayOnLan.Api.Communication
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message, object client)
        {
            this.Message = message;
            Client = client;
        }

        public string Message { get; set; }

        public object Client { get; set; }
    }
}