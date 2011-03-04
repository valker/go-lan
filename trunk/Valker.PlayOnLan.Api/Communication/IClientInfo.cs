using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Api.Communication
{
    public interface IClientInfo : IEquatable<IClientInfo>
    {
        IMessageConnector Connector { get; }
        object Identifier { get; }
    }
}
