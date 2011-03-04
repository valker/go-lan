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
        IMessageConnector ClientConnector { get; }
        object ClientIdentifier { get; }
    }
}
