using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public abstract class SingleServerMessage : ServerMessage
    {
        public SingleServerMessage()
        {
        }

        public SingleServerMessage(IMessageConnector connector)
        {
            this.Connector = connector;
        }

        protected IMessageConnector Connector { get; set; }
    }
}