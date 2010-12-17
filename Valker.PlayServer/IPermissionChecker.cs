namespace Valker.PlayServer
{
    internal interface IPermissionChecker
    {
        bool IsAllowed(IClient client, Permissions permissions);
    }
}