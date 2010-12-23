using System;
using agsXMPP;
using agsXMPP.protocol.Base;
using Valker.Api;

namespace Valker.PlayOnLan.Transport
{
    internal class XmppClient : IClient
    {
        public Jid Jid { get; private set; }

        public string Name { get; private set; }

        public event EventHandler Closed;

        public XmppClient(Item item)
        {
            Jid = item.Jid;
            Name = item.Name;
        }

        public XmppClient(Jid jid, string name)
        {
            Jid = jid;
            Name = name;
        }
    }
}