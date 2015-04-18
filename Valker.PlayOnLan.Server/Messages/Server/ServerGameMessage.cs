using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Server;

namespace Valker.PlayOnLan.Server2008.Messages.Server
{
    public class ServerGameMessage : ServerMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ServerMessage), new []{typeof(ServerGameMessage)});

        public ServerGameMessage(string s)
        {
            Text = s;
        }

        public ServerGameMessage()
        {
        }

        public string Text { get; set; }

        public int PartyId { get; set; }

        public override XmlSerializer GetSerializer()
        {
            return Serializer;
        }

        public override void Execute(IServerMessageExecuter server, IAgentInfo sender)
        {
            server.ExecuteServerGameMessage(sender, Text, PartyId);
        }
    }
}
