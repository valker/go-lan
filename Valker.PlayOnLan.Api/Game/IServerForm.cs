using System;

namespace Valker.PlayOnLan.Api.Game
{
    /// <summary>
    /// Define an interface for common server form (independent for differents gui engines)
    /// </summary>
    public interface IServerForm : IForm
    {
        event EventHandler<NewAgentCreatingEventArgs> NewAgentCreating;
    }
}