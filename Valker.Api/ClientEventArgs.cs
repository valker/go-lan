using System;

namespace Valker.Api
{
    public class ClientEventArgs : EventArgs
    {
        public IClient Client { get; set; }
    }
}