using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    /// <summary>
    /// Defines a base class of message that is received by client
    /// </summary>
    public abstract class ClientMessage : Message
    {
        public abstract void Execute(IClientMessageExecuter client, IMessageConnector sender);
    }
}