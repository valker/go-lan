using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    class RegisterNewPartyMessage : ServerMessage
    {
        #region Overrides of ServerMessage

        public override void Execute(IServerMessageExecuter server)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
