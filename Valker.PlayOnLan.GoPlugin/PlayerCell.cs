using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    public class PlayerCell : ICell
    {
        protected bool Equals(PlayerCell other)
        {
            return Equals(Player, other.Player);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PlayerCell) obj);
        }

        public override int GetHashCode()
        {
            return (Player != null ? Player.GetHashCode() : 0);
        }

        public PlayerCell(IPlayer player)
        {
            Player = player;
        }

        public override string ToString()
        {
            return string.Format("PLAYER:{0}", Player.PlayerName);
        }

        public IPlayer Player { get; set; }
    }
}