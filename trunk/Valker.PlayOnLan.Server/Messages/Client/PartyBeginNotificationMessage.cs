using System;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public sealed class PartyBeginNotificationMessage : ClientMessage
    {
        private static XmlSerializer Serializer = new XmlSerializer(typeof(ClientMessage), new[]{typeof(PartyBeginNotificationMessage)});

        #region Overrides of ClientMessage

        public override void Execute(IClientMessageExecuter client, object sender)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
