using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Api.Game
{
    public interface IGameServer
    {
        /// <summary>
        /// Register new party
        /// </summary>
        /// <remarks>TODO: parameters should be defined</remarks>
        void RegisterNewParty(string playerName, IGameParameters parameters);
    }
}
