using System;
using System.Collections.Generic;

namespace Valker.PlayOnLan.Api.Game
{
    public sealed class OnMessageEventArgs : EventArgs
    {
        public IEnumerable<IPlayer> Receipients { get; set; }

        public string Message { get; private set; }
    }
}