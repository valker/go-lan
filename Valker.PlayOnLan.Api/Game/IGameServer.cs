using System;

namespace Valker.PlayOnLan.Api.Game
{
    public interface IGameServer
    {
        void ProcessMessage(IPlayer sender, string message);
        event EventHandler<OnMessageEventArgs> OnMessage;
        void Start();
    }
}
