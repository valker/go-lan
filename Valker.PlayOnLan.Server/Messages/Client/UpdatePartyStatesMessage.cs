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
            if (requests == null) throw new ArgumentNullException();
            this.Info = requests.ToArray();
        }

        public PartyState[] Info { get; set; }

        #region Overrides of ClientMessage

        public override void Execute(IClientMessageExecuter client, object sender)
        {
            if (client == null) throw new ArgumentNullException();
            var connector = sender as IMessageConnector;
            if (connector == null) throw new ArgumentException();
            client.UpdatePartyStates(Info, connector);
        }

        #endregion
    }
}