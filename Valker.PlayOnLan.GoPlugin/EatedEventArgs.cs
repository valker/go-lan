using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Valker.PlayOnLan.GoPlugin
{
    public class EatedEventArgs : EventArgs
    {
        public EatedEventArgs(IEnumerable<string> strings)
        {
            Eated = strings.Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
        }

        public double[] Eated { get; private set; }
    }
}