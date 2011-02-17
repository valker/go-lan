using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public abstract class ClientMessage : Message
    {
        public abstract void Execute(IClientMessageExecuter client, object sender);

        protected override Type GetBaseClass()
        {
            return typeof (ClientMessage);
        }
    }
}