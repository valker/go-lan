using System;
using System.Collections.Generic;
using System.Linq;

namespace Valker.PlayOnLan.GoPlugin
{
    public class EatedEventArgs : EventArgs
    {
        public EatedEventArgs(IEnumerable<string> strings)
        {
            Eated = strings.Select(s => int.Parse(s)).ToArray();
        }

        public int[] Eated { get; set; }
    }
}