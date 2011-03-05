using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Api.Game
{
    public interface IPlayer
    {
        IClientInfo Client { get; set; }
        string PlayerName { get; set; }
    }
}
