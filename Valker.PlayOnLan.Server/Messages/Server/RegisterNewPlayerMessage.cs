using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public class RegisterNewPlayerMessage : ServerMessage
    {
        public override void Execute(IServerMessageExecuter server, IClientInfo sender)
        {
            server.RegisterNewPlayer(Name, sender);
        }

        public string Name { get; set; }
    }
}
