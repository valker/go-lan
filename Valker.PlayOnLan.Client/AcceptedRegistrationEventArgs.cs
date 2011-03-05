using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Client
{
    public class AcceptedRegistrationEventArgs : EventArgs
    {
        public bool Status { get; set; }
    }
}
