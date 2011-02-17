using System;

namespace Valker.PlayOnLan.Api.Communication
{
    public abstract class MessageExecuter { }

    public abstract class ClientMessageExecuter : MessageExecuter {
        public abstract void UpdateSupportedGames(string[] games);
    }

    public abstract class ServerMessageExecuter : MessageExecuter {
        public abstract string[] RetrieveSupportedGames();
        public abstract void Send(string message);
    }
}