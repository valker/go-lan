using System;

namespace Valker.PlayOnLan.Engine
{
    public class NeiboursChangedEventArgs : EventArgs
    {
        public bool Added { get; set; }
        public INeibour Neibour { get; set; }
    }
}