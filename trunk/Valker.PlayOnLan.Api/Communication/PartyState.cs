using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Api.Communication
{
    public class PartyState
    {
        public PartyState()
        {
            
        }

        public PartyStatus Status { get; set; }

        public string Name { get; set; }

        public string GameId { get; set; }
    }
}
