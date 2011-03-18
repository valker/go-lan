using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.Server
{
    public class Player : IPlayer
    {
        #region Implementation of IPlayer

        public IAgentInfo Agent
        {
            get; set;
        }

        public string PlayerName
        {
            get; set;
        }

        #endregion
    }
}