using System;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public sealed class PartyBeginNotificationMessage : ClientMessage
    {
        private static XmlSerializer Serializer = new XmlSerializer(typeof(ClientMessage), new[]{typeof(PartyBeginNotificationMessage)});

        public PartyBeginNotificationMessage()
        {
        }

        public PartyBeginNotificationMessage(int partyId, string gameTypeId, string parameters)
        {
            GameTypeId = gameTypeId;
            PartyId = partyId;
            Parameters = parameters;
        }

        public string GameTypeId { get; set; }

        public int PartyId { get; set; }

        #region Overrides of ClientMessage

        public override void Execute(IClientMessageExecuter client, object sender)
        {
            client.PartyBeginNotification(PartyId, GameTypeId, Parameters);
        }

        public string Parameters { get; set; }

        #endregion

        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
