using System;
using System.Xml.Serialization;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public class AcceptNewPlayerMessage : ClientMessage
    {
        private static XmlSerializer Serializer = new XmlSerializer(typeof(ClientMessage), new[]{ typeof(AcceptNewPlayerMessage)});

        public override void Execute(Api.Communication.IClientMessageExecuter client, object sender)
        {
            if (client == null) throw new ArgumentNullException();
            client.AcceptNewPlayer(Status);
        }

        public bool Status { get; set; }
        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
