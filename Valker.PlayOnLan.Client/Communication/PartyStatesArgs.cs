using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Client.Communication
{
    public class PartyStatesArgs : EventArgs
    {
        public PartyStatesArgs(PartyState[] states, IMessageConnector connector)
        {
            this.States = states;
            this.Connector = connector;
        }

        public IMessageConnector Connector { get; set; }

        public PartyState[] States { get; set; }
    }
}