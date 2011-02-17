using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    class AcknowledgePartyBeginMessage : ClientMessage
    {
        #region Overrides of ClientMessage

        public override void Execute(IClientMessageExecuter client, object sender)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
