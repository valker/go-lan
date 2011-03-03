using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Client.Communication
{
    public class AcceptedPlayerEventArgs : EventArgs
    {
        public bool Status { get; set; }
    }
}
