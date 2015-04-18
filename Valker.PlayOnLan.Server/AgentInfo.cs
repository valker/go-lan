using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server
{
    public class AgentInfo : IAgentInfo
    {
        #region IAgentInfo Members

        public IMessageConnector ClientConnector
        {
            get; set;
        }

        public object ClientIdentifier
        {
            get; set;
        }

        #endregion

        #region IEquatable<IAgentInfo> Members

        public bool Equals(IAgentInfo other)
        {
            return ClientConnector.Equals(other.ClientConnector) 
                && ClientIdentifier.Equals(other.ClientIdentifier);
        }

        #endregion
    }
}
