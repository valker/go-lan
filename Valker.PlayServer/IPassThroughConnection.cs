using System;

namespace Valker.PlayServer
{
    internal interface IPassThroughConnection
    {
        event EventHandler Closed;

        void Start();

        IClient Acquirer { get; }

        IIdentifiedClient Announcer { get; }
    }
}