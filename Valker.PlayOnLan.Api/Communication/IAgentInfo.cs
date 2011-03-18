using System;

namespace Valker.PlayOnLan.Api.Communication
{
    /// <summary>
    /// Identify the agent that could send messages to each other
    /// <remarks>Identifier could vary in depends on the transport</remarks>
    /// </summary>
    public interface IAgentInfo : IEquatable<IAgentInfo>
    {
        /// <summary>
        /// Connector which is used by client (at server side)
        /// </summary>
        IMessageConnector ClientConnector { get; }

        /// <summary>
        /// Identifier of client (depends on the transport)
        /// </summary>
        object ClientIdentifier { get; }
    }
}
