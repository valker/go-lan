using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Server;

namespace Valker.PlayOnLan.Server2008.Messages.Server
{
    public class UnregisterPlayerMessage : ServerMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ServerMessage), new[] { typeof(UnregisterPlayerMessage) });

        public override XmlSerializer GetSerializer()
        {
            return Serializer;
        }

        public override void Execute(IServerMessageExecuter server, IAgentInfo sender)
        {
            server.UnregisterPlayer(sender);
        }
    }
}
