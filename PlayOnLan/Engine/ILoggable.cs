using System;
using GoLanClient.Engine;
using Valker.Api;

namespace Valker.PlayOnLan.Engine
{
    public interface ILoggable
    {
        event EventHandler<OnMessageEventArgs> OnMessage;
    }
}