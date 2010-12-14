using System;
namespace Valker.PlayServer
{
    /// <summary>
    /// Define an interface of object that should establish connection with clients
    /// </summary>
    public interface IConnectionEstablisher
    {
        /// <summary>
        /// This event is raised when new client is connected to this server
        /// </summary>
        event EventHandler<ConnectionEstablishedEventArgs> ConnectionEstablished;

        /// <summary>
        /// Gets the port
        /// </summary>
        int Port { get; }

        /// <summary>
        /// Start listening clients
        /// </summary>
        /// <param name="port">Port used</param>
        void Start(int port);

        /// <summary>
        /// Stop listening clients
        /// </summary>
        void Stop();
    }
}