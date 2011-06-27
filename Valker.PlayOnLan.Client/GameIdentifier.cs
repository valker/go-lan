using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Client2008
{
    public class PartyInfo : IEquatable<PartyInfo>
    {
        public override string ToString()
        {
            return Name + ' ' + GameName;
        }

        public string Name { get; set; }
        public string GameTypeId { get; set; }
        public string GameName { get; set; }
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
            return Equals(other.Name, Name) && Equals(other.GameTypeId, GameTypeId) && Equals(other.Connector, Connector) && Equals(other.Status, Status);
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
            return Equals((PartyInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Name.GetHashCode();
                result = (result * 397) ^ GameTypeId.GetHashCode();
                result = (result * 397) ^ Connector.GetHashCode();
                result = (result * 397) ^ Status.GetHashCode();
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