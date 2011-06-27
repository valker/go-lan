using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Api.Game
{
    public interface IPlayerBase
    {
        string PlayerName { get; set; }
    }

    public interface IPlayer : IPlayerBase
    {
        IAgentInfo Agent { get; set; }
    }
}
