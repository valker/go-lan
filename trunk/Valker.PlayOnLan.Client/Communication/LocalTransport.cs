using System.Collections.Generic;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Client.Communication
{
    public class LocalTransport
    {
        List<LocalMessageConnector> connectors = new List<LocalMessageConnector>();

        public IMessageConnector CreateMessageConnector(string name)
        {
            var connector = new LocalMessageConnector(this, name);
            this.connectors.Add(connector);
            return connector;
        }

        public void SendMessage(LocalMessageConnector connector, string message)
        {
            foreach (var localMessageConnector in connectors)
            {
                if (!ReferenceEquals(localMessageConnector, connector))
                {
                    localMessageConnector.OnMessageArrived(message);
                }
            }
        }
    }
}