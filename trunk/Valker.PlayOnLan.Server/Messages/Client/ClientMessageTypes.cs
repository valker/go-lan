using System;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public static class ClientMessageTypes
    {
        private static readonly Type[] _types = new[]
                                                {
                                                    typeof (RetrieveSupportedGamesResponceMessage),
                                                    typeof (AcknowledgeRegistrationMessage),
                                                    typeof (AcknowledgeDropMessage), 
                                                    typeof (PartyBeginNotificationMessage),
                                                    typeof (UpdatePartyStatesMessage), 
                                                    typeof(AcceptNewPlayerMessage),
                                                };

        public static Type[] Types
        {
            get { return _types; }
        }
    }
}