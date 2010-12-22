using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.Api
{
    /// <summary>
    /// Define an interface to abstract different message servers
    /// </summary>
    public interface ITransport
    {
        void Start();
        event EventHandler<ClientEventArgs> ClientAdded;
        event EventHandler<ClientEventArgs> ClientRemoved;
        string Name { get; }
    }
}
