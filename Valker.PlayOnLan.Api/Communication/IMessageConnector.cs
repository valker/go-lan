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
        /// <param name="fromIdentifier">from who this message</param>
        /// <param name="toIdentifier">to who this message</param>
        void Send(object fromIdentifier, object toIdentifier, string message);

        /// <summary>
        /// Turn on checking that the client identified by given identifier become disconnected
        /// </summary>
        /// <param name="identifier">identifier of the client</param>
        void WatchOther(object identifier);

        /// <summary>
        /// Raised when other (watched subject) has been disconnected
        /// </summary>
        event EventHandler<DisconnectedClientEventArgs> DisconnectedOther;

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