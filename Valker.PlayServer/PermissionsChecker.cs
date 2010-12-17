using System.Collections.Generic;

namespace Valker.PlayServer
{
    internal class PermissionsChecker : IPermissionChecker
    {
        private readonly IDictionary<IClient, ICollection<Permissions>> _permissions =
            new Dictionary<IClient, ICollection<Permissions>>();

        #region IPermissionChecker Members

        public bool IsAllowed(IClient client, Permissions permissions)
        {
            if (!_permissions.ContainsKey(client)) return false;
            if (permissions == Permissions.None) return true;
            ICollection<Permissions> permiss = _permissions[client];
            if (permiss == null)
            {
                return false;
            }

            bool value = permiss.Contains(permissions);

            return value;
        }

        #endregion

        public void GrantPermissions(IClient client, Permissions permissions)
        {
            ICollection<Permissions> permissionses = GetPermissionses(client);
            if (permissionses.Contains(permissions)) return;
            permissionses.Add(permissions);
        }

        public void RemovePermissions(IClient client, Permissions permissions)
        {
            ICollection<Permissions> permissionses = GetPermissionses(client);
            if (!permissionses.Contains(permissions)) return;
            permissionses.Remove(permissions);
        }

        private static List<Permissions> CreatePermissionsCollection()
        {
            return new List<Permissions>();
        }

        private ICollection<Permissions> GetPermissionses(IClient client)
        {
            ICollection<Permissions> permissionses;
            if (!_permissions.ContainsKey(client))
            {
                permissionses = CreatePermissionsCollection();
                _permissions.Add(client, permissionses);
            }
            else
            {
                permissionses = _permissions[client];
            }
            if(permissionses == null)
            {
                permissionses = CreatePermissionsCollection();
                _permissions[client] = permissionses;
            }
            return permissionses;
        }
    }
}