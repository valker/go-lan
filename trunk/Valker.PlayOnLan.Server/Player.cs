using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server
{
    public class Player : IPlayer
    {
        #region Implementation of IPlayer

        public IMessageConnector connector
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion
    }
}