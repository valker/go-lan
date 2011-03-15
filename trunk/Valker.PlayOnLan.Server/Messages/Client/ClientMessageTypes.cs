using System;
using System.Xml.Serialization;
using Valker.PlayOnLan.Server2008.Messages.Client;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public static class ClientMessageTypes
    {
        private static readonly Type[] Types = new[]
                                                {
                                                    typeof (RetrieveSupportedGamesResponceMessage),
                                                    typeof (AcknowledgeRegistrationMessage),
                                                    typeof (AcknowledgeDropMessage), 
                                                    typeof (PartyBeginNotificationMessage),
                                                    typeof (UpdatePartyStatesMessage), 
                                                    typeof(AcceptNewPlayerMessage),
                                                    typeof(ClientGameMessage),
                                                };

        private static readonly XmlSerializer _serializer = new XmlSerializer(typeof (ClientMessage), Types);

        public static XmlSerializer Serializer { get { return _serializer; } }
    }
}