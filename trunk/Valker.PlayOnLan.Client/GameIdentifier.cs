using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Client
{
    class PartyInfo : IEquatable<PartyInfo>
    {
        public string Name { get; set; }
        public string GameTypeId { get; set; }
        public PartyStatus Status { get; set; }

        public bool Equals(PartyInfo other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.Name, this.Name) && Equals(other.GameTypeId, this.GameTypeId) && Equals(other.Connector, this.Connector) && Equals(other.Status, Status);
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
            if (obj.GetType() != typeof (PartyInfo))
            {
                return false;
            }
            return this.Equals((PartyInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = this.Name.GetHashCode();
                result = (result * 397) ^ this.GameTypeId.GetHashCode();
                result = (result * 397) ^ this.Connector.GetHashCode();
                result = (result * 397) ^ this.Status.GetHashCode();
                return result;
            }
        }

        public static bool operator ==(PartyInfo left, PartyInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PartyInfo left, PartyInfo right)
        {
            return !Equals(left, right);
        }

        public IMessageConnector Connector { get; set; }

        public int PartyId { get; set; }
    }
}