using System;

namespace Valker.PlayServer
{
    public class ReadCompletedEventArgs : EventArgs
    {
        public byte[] Buffer
        {
            get;
            set;
        }

        public int BytesRead
        {
            get;
            set;
        }
    }
}