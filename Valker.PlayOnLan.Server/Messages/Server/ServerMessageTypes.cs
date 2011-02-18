using System;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public static class ServerMessageTypes
    {
        private static readonly Type[] _types = new[]
                                                {
                                                    typeof (RetrieveSupportedGamesMessage),
                                                    typeof (RegisterNewPartyMessage), typeof (AcceptNewPartyMessage),
                                                    typeof (DropNewPartyMessage), typeof (RetrieveRegistredPartiesMessage),
                                                };

        public static Type[] Types
        {
            get { return _types; }
        }
    }
}