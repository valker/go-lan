using System;

namespace Valker.PlayOnLan.Api.Communication
{
    /// <summary>
    /// Defines an interface of the thing that allows to send and receive messages
    /// </summary>
    public interface IMessageConnector
    {
        string Name { get; set; }
        void SendMessage(string message);

        event EventHandler<MessageEventArgs> MessageArrived;
    }
}