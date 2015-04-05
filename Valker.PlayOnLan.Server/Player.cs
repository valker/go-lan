using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.Server
{
    public class Player : IPlayer
    {
        protected bool Equals(Player other)
        {
            return string.Equals(PlayerName, other.PlayerName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Player) obj);
        }

        public override int GetHashCode()
        {
            return (PlayerName != null ? PlayerName.GetHashCode() : 0);
        }

        #region Implementation of IPlayer

        public IAgentInfo Agent
        {
            get; set;
        }

        public int Order { get; set; }

        public string PlayerName
        {
            get; set;
        }

        #endregion
    }
}