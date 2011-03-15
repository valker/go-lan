using System;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public class DropNewPartyMessage : ServerMessage
    {
        private static XmlSerializer Serializer = new XmlSerializer(typeof(ServerMessage), new[]{typeof(DropNewPartyMessage)});

        #region Overrides of ServerMessage

        public override void Execute(IServerMessageExecuter server, IClientInfo sender)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
