using System;

namespace Valker.PlayOnLan.Server.Messages
{
    public static class ServerMessageTypes
    {
        private static readonly Type[] _types = new[] {typeof (RetrieveSupportedGamesMessage)};

        public static Type[] Types
        {
            get { return _types; }
        }
    }
}