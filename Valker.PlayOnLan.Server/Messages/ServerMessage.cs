using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages
{
    public abstract class ServerMessage : Message
    {
        public abstract void Execute(IServerMessageExecuter server);
        protected override Type GetBaseClass()
        {
            return typeof (ServerMessage);
        }
    }
}