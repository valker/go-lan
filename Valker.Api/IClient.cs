using System;

namespace Valker.Api
{
    public interface IClient
    {
        string Name { get; }
        event EventHandler Closed;
    }
}