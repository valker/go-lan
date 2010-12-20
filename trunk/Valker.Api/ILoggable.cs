using System;

namespace Valker.Api
{
    public class OnMessageEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public interface ILoggable
    {
        event EventHandler<OnMessageEventArgs> OnMessage;
    }
}