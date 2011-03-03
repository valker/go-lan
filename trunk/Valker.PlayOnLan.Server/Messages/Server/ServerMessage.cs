using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public abstract class ServerMessage : Message
    {
        public abstract void Execute(IServerMessageExecuter server, IClientInfo sender);

        protected override Type GetBaseClass()
        {
            return typeof (ServerMessage);
        }
    }
}