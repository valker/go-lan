using System;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public static class ClientMessageTypes
    {
        private static readonly Type[] _types = new[]
                                                {
                                                    typeof (RetrieveSupportedGamesResponceMessage),
                                                    typeof (AcknowledgeRegistrationMessage),
                                                    typeof (AcknowledgeDropMessage), typeof (AcknowledgePartyBeginMessage),
                                                    typeof (PartyBeginNotificationMessage),
                                                    typeof (UpdatePartyStatesMessage),
                                                };

        public static Type[] Types
        {
            get { return _types; }
        }
    }
}