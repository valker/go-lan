using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Api.Communication
{
    /// <summary>
    /// Identify the client connected to the server
    /// <remarks>Identifier could vary in depends on the transport</remarks>
    /// </summary>
    public interface IClientInfo : IEquatable<IClientInfo>
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
