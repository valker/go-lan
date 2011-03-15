using System;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Server;

namespace Valker.PlayOnLan.Server2008.Messages.Server
{
    public class ServerGameMessage : ServerMessage
    {
        private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(ServerMessage), new []{typeof(ServerGameMessage)});

        public override XmlSerializer GetSerializer()
        {
            return _serializer;
        }

        public override void Execute(IServerMessageExecuter server, IClientInfo sender)
        {
            throw new NotImplementedException();
        }
    }
}
