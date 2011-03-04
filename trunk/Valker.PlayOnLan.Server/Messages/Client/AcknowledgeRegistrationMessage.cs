using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public class AcknowledgeRegistrationMessage : ClientMessage
    {
        public AcknowledgeRegistrationMessage()
        {
        }

        public AcknowledgeRegistrationMessage(bool status)
        {
            Status = status;
        }

        public bool Status { get; set; }

        #region Overrides of ClientMessage

        public override void Execute(IClientMessageExecuter client, object sender)
        {
            //var text = Status ? "Registred OK" : "Didn't registred";
            //client.ShowMessage(text);
        }

        #endregion
    }
}
