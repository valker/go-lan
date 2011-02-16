using System.Collections.Generic;

namespace Valker.PlayServer
{
    class Room : IRoom 
    {
        List<IClient> _clients = new List<IClient>();

        public Room(string name)
        {
            Name = name;
            PermissionChecker = new PermissionsChecker();
        }

        public Room(string name, IClient creator) : this(name)
        {
            Name = name;
            PermissionsChecker checker = new PermissionsChecker();
            checker.GrantPermissions(creator, Permissions.All);
            PermissionChecker = checker;
        }

        /// <summary>
        /// Add new client to the room
        /// </summary>
        /// <param name="client">client</param>
        public void AddClient(IClient client)
        {
            _clients.Add(client);
        }

        /// <summary>
        /// Remove given client from the room
        /// </summary>
        /// <param name="client">client</param>
        public void RemoveClient(IClient client)
        {
            _clients.Remove(client);
        }

        /// <summary>
        /// Gets list of clients
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IClient> GetClients()
        {
            return _clients;
        }

        /// <summary>
        /// Gets the name of the room
        /// </summary>
        public string Name { get; private set; }

        public IPermissionChecker PermissionChecker
        {
            get; private set;
        }
    }
}