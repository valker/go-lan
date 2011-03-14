using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    /// <summary>
    /// Define a base class of message that is received by server
    /// </summary>
    public abstract class ServerMessage : Message
    {
        public abstract void Execute(IServerMessageExecuter server, IClientInfo sender);
    }
}