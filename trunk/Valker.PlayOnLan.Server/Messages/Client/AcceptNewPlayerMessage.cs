using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public class AcceptNewPlayerMessage : ClientMessage
    {
        public override void Execute(Api.Communication.IClientMessageExecuter client, object sender)
        {
            if (client == null) throw new ArgumentNullException();
            client.AcceptNewPlayer(Status);
        }

        public bool Status { get; set; }
    }
}
