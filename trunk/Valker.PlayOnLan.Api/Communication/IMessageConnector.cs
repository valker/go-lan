using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Api.Communication
{
    /// <summary>
    /// Defines an interface of the thing that allows to send and receive messages
    /// </summary>
    public interface IMessageConnector
    {
        void SendMessage(string message);

        event EventHandler<MessageEventArgs> MessageArrived;
    }
}
