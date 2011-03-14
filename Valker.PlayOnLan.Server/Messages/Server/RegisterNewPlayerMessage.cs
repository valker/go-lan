using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public class RegisterNewPlayerMessage : ServerMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ServerMessage), new[]{typeof(RegisterNewPlayerMessage)});

        public override void Execute(IServerMessageExecuter server, IClientInfo sender)
        {
            if (server == null) throw new ArgumentNullException();
            server.RegisterNewPlayer(sender, Name);
        }

        public string Name { get; set; }
        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
