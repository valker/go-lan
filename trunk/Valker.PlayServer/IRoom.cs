using System.Collections.Generic;

namespace Valker.PlayServer
{
    /// <summary>
    /// Defines an interface of room
    /// </summary>
    internal interface IRoom
    {
        /// <summary>
        /// Add new client to the room
        /// </summary>
        /// <param name="client">client</param>
        void AddClient(IClient client);

        /// <summary>
        /// Remove given client from the room
        /// </summary>
        /// <param name="client">client</param>
        void RemoveClient(IClient client);

        /// <summary>
        /// Gets list of clients
        /// </summary>
        /// <returns></returns>
        IEnumerable<IClient> GetClients();

        /// <summary>
        /// Gets the name of the room
        /// </summary>
        string Name { get; }

        IPermissionChecker PermissionChecker { get; }
    }
}