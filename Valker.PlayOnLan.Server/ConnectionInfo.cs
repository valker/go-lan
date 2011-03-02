using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server
{
    class ConnectionInfo
    {
        public IMessageConnector Connector { get; set; }
    }
}
