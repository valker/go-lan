using System;
using Valker.PlayOnLan.Server2008.Messages.Server;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public static class ServerMessageTypes
    {
        private static readonly Type[] _types = new[]
                                                {
                                                    typeof (RetrieveSupportedGamesMessage),
                                                    typeof (RegisterNewPartyMessage), 
                                                    typeof (AcceptNewPartyMessage),
                                                    typeof (DropNewPartyMessage), 
                                                    typeof (RetrieveRegistredPartiesMessage), 
                                                    typeof (RegisterNewPlayerMessage), 
                                                    typeof (ServerGameMessage),
                                                };

        public static Type[] Types
        {
            get { return _types; }
        }
    }
}