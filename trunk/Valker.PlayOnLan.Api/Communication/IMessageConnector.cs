using System;

namespace Valker.PlayOnLan.Api.Communication
{
    /// <summary>
    /// Defines an interface of the thing that allows to send and receive messages
    /// </summary>
    public interface IMessageConnector : IDisposable
    {
        /// <summary>
        /// Identifier of connection type (local, xmpp, etc.)
        /// </summary>
        string ConnectorName { get; set; }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message">message to be sent</param>
        /// <param name="Recepient">destination of the message</param>
        void Send(object Recepient, string message);

        /// <summary>
        /// Client identifiers connected to this connector
        /// <remark>used in server part</remark>
        /// </summary>
        string[] Clients { get; }

        /// <summary>
        /// Message from one of the client arrived (contains message and sender id)
        /// </summary>
        event EventHandler<MessageEventArgs> MessageArrived;

        /// <summary>
        /// Raised when connection is destroyed
        /// </summary>
        event EventHandler Closed;
    }
}