using System;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.Client.Communication
{
    public class PartyStatesArgs : EventArgs
    {
        public PartyStatesArgs(PartyState[] states, IMessageConnector connector)
        {
            States = states;
            Connector = connector;
        }

        public IMessageConnector Connector { get; set; }

        public PartyState[] States { get; set; }
    }
}