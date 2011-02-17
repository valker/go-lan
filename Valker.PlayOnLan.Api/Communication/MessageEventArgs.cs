using System;

namespace Valker.PlayOnLan.Api.Communication
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}