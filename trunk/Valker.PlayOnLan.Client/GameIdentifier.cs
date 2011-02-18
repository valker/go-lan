using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Client
{
    class GameIdentifier : IEquatable<GameIdentifier>
    {
        public string Name { get; set; }
        public string GameType { get; set; }

        public bool Equals(GameIdentifier other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.Name, this.Name) && Equals(other.GameType, this.GameType) && Equals(other.Connector, this.Connector);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof (GameIdentifier))
            {
                return false;
            }
            return this.Equals((GameIdentifier) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = this.Name.GetHashCode();
                result = (result * 397) ^ this.GameType.GetHashCode();
                result = (result * 397) ^ this.Connector.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(GameIdentifier left, GameIdentifier right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GameIdentifier left, GameIdentifier right)
        {
            return !Equals(left, right);
        }

        public IMessageConnector Connector { get; set; }
    }
}