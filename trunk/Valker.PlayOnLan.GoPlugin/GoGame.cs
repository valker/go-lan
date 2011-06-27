using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public class GoGame : IGameType
    {
        public string Name
        {
            get { return "Go"; }
        }

        public string Id
        {
            get { return "19873C0D-1E8B-40ce-9633-590A5601ECFD"; }
        }

        public string AskParam(IForm parent)
        {
            var p = new Parameters();
            p.Width = 19;
            return p.ToString();
        }

        public IGameClient CreateClient(IForm parent)
        {
            return new GoClient(parent);
        }

        public IGameServer CreateServer(IPlayer[] players, string parameters)
        {
            return new GoServer(players, parameters);
        }
    }
}
