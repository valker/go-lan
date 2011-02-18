using System;
using System.Collections.Generic;
using System.Linq;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public class UpdatePartyStatesMessage : ClientMessage
    {
        public UpdatePartyStatesMessage()
        {
        }

        public UpdatePartyStatesMessage(List<PartyState> requests)
        {
            this.Info = requests.Where(state => state.Status == PartyStatus.PartyRegistred).ToArray();
        }

        public PartyState[] Info { get; set; }

        #region Overrides of ClientMessage

        public override void Execute(IClientMessageExecuter client, object sender)
        {
            client.UpdatePartyStates(Info, (IMessageConnector) sender);
        }

        #endregion
    }
}