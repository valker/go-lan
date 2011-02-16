using System;
using System.Runtime.CompilerServices;

namespace Valker.PlayServer
{
    internal class MessageReceivedEventArgs : EventArgs
    {
        public IClient Client
        {
            get;set;
        }

        public string Message
        {
            get;set;
        }
    }
}