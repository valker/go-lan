using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// Define the 'Go' ('baduk', 'weiqi') game for PlayOnLan framework
    /// </summary>
    public class GoGame : IGameType
    {
        /// <summary>
        /// Describes the name of the game on original language
        /// </summary>
        public string Name
        {
            get { return "Go"; }
        }

        /// <summary>
        /// Defines the unique identifier of the game type
        /// </summary>
        public string Id
        {
            get { return "19873C0D-1E8B-40ce-9633-590A5601ECFD"; }
        }

        /// <summary>
        /// Ask user about parameters of the game. It depends on the game type
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public string AskParam(IForm parent)
        {
            var p = new Parameters {Width = 9};
            return p.ToString();
        }

        /// <summary>
        /// Creates client component of the game
        /// </summary>
        /// <returns></returns>
        public IGameClient CreateClient(IForm parent)
        {
            return new GoClient();
        }

        /// <summary>
        /// Creates server component of the game
        /// </summary>
        /// <param name="players">array of player, which will participate</param>
        /// <param name="parameters">parameters of the game depended on the game type</param>
        /// <returns>the server componend of the game</returns>
        public IGameServer CreateServer(IPlayer[] players, string parameters)
        {
            return new GoServer(players, parameters);
        }
    }
}
