using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server
{
    class ClientInfo : IClientInfo
    {
        #region IClientInfo Members

        public IMessageConnector ClientConnector
        {
            get; set;
        }

        public object ClientIdentifier
        {
            get; set;
        }

        #endregion

        #region IEquatable<IClientInfo> Members

        public bool Equals(IClientInfo other)
        {
            return this.ClientConnector.Equals(other.ClientConnector) 
                && this.ClientIdentifier.Equals(other.ClientIdentifier);
        }

        #endregion
    }
}
