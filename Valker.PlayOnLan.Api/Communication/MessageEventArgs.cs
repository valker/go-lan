using System;

namespace Valker.PlayOnLan.Api.Communication
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message, string client)
        {
            this.Message = message;
            Client = client;
        }

        public string Message { get; set; }

        public string Client { get; set; }
    }
}