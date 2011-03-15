using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Api.Game
{
    public interface IGameServer
    {
        void ProcessMessage(IPlayer sender, string message);
        event EventHandler<OnMessageEventArgs> OnMessage;
    }
}
