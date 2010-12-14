using System;
using System.Collections.Generic;

namespace Valker.PlayServer
{
    internal class ListenerCollection : IListenerCollection
    {
        private readonly List<IClient> _clents = new List<IClient>();

        #region IListenerCollection Members

        public void Add(IClient item)
        {
            lock (_clents)
            {
                _clents.Add(item);
            }
            item.Disconnected += ItemOnDisconnected;
        }

        public void SendMessage(string message)
        {
            IClient[] clientArray;
            message = message + "\r\n";
            lock (_clents)
            {
                clientArray = _clents.ToArray();
            }
            foreach (IClient client in clientArray)
            {
                client.WriteAsync(message);
            }
        }

        #endregion

        private void ItemOnDisconnected(object sender, EventArgs args)
        {
            var item = (IClient) sender;
            item.Disconnected -= ItemOnDisconnected;
            lock (_clents)
            {
                _clents.Remove(item);
            }
        }
    }
}