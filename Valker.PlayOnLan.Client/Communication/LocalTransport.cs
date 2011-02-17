using System.Collections.Generic;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Client.Communication
{
    public class LocalTransport
    {
        private List<LocalMessageConnector> connectors = new List<LocalMessageConnector>();

        public IMessageConnector CreateMessageConnector(string name)
        {
            var connector = new LocalMessageConnector(this, name);
            this.connectors.Add(connector);
            return connector;
        }

        public void SendMessage(LocalMessageConnector connector, string message)
        {
            foreach (LocalMessageConnector localMessageConnector in this.connectors)
            {
                if (!ReferenceEquals(localMessageConnector, connector))
                {
                    localMessageConnector.OnMessageArrived(message);
                }
            }
        }
    }
}