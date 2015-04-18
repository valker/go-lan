using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Client;

namespace Valker.PlayOnLan.Server2008.Messages.Client
{
    /// <summary>
    /// This message is sent from server to client and contains game specific message
    /// </summary>
    public class ClientGameMessage : ClientMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ClientMessage), new []{typeof(ClientGameMessage)});

        public ClientGameMessage(string msg)
        {
            Message = msg;
        }

        public ClientGameMessage()
        {
        }

        public string Message { get; set; }

        public override XmlSerializer GetSerializer()
        {
            return Serializer;
        }

        public override void Execute(IClientMessageExecuter client, IMessageConnector sender)
        {
            client.ExecuteGameMessage(sender, Message);
        }
    }
}
