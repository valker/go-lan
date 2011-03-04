using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Valker.PlayOnLan.Api.Communication
{
    public class PartyState
    {
        public PartyState()
        {
            
        }

        public PartyStatus Status { get; set; }

        [XmlIgnore]
        public IPlayer[] players { get; set; }

        public string[] playerNames { get; set; }

        public string GameTypeId { get; set; }

        public void Dispose()
        {
            // todo : implement notification of players
        }
    }

    public interface IPlayer
    {
        IClientInfo Client { get; set; }
        string Name { get; set; }
    }
}
