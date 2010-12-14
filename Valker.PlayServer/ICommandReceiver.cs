using System;

namespace Valker.PlayServer
{
    public interface ICommandReceiver
    {
        event EventHandler MessageReceived;

        void Start(IClient client);
    }
}