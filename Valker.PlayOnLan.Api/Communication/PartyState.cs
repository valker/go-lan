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

        public IPlayer[] players { get; set; }

        public string GameTypeId { get; set; }
    }

    public interface IPlayer
    {
        IMessageConnector connector { get; set; }
        string Name { get; set; }
    }
}
