using System;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public class AcceptNewPartyMessage : ServerMessage
    {
        private static XmlSerializer Serializer = new XmlSerializer(typeof(ServerMessage), new[]{typeof(AcceptNewPartyMessage)});

        public string AccepterName { get; set; }

        public int PartyId { get; set; }

        public AcceptNewPartyMessage()
        {

        }

        public AcceptNewPartyMessage(int partyId, string accepterName)
        {
            PartyId = partyId;
            AccepterName = accepterName;
        }

        #region Overrides of ServerMessage

        public override void Execute(IServerMessageExecuter server, IClientInfo sender)
        {
            if (server == null) throw new ArgumentNullException();
            server.AcceptPartyRequest(PartyId, AccepterName);
        }

        #endregion

        public override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
