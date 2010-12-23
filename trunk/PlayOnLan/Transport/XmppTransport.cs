using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq.disco;
using agsXMPP.protocol.iq.roster;
using agsXMPP.Xml.Dom;
using Valker.Api;
using Message=agsXMPP.protocol.client.Message;
using RosterItem=agsXMPP.protocol.iq.roster.RosterItem;

namespace Valker.PlayOnLan.Transport
{
    class XmppTransport : ITransport 
    {
        private XmppClientConnection _connection = new XmppClientConnection();
        private DiscoManager _disco;
        private List<RosterItem> _rosterList = new List<RosterItem>();
        private List<RosterItem> _tmpRosterList;


        public void Start()
        {
            Jid jid = new Jid("admin@MOSDB9VF4J");
            _connection.Password = "12345";
            _connection.Username = jid.User;
            _connection.Server = jid.Server;
            _connection.AutoAgents = false;
            _connection.AutoPresence = true;
            _connection.AutoRoster = true;
            _connection.AutoResolveConnectServer = true;
            _connection.OnRosterStart += OnRosterStart;
            _connection.OnRosterItem += OnRosterItem;
            _connection.OnRosterEnd += OnRosterEnd;
            _connection.OnPresence += OnPresence;
            _connection.OnMessage += OnMessage;
            _connection.OnLogin += OnLogin;

            _disco = new DiscoManager(_connection);

            _connection.Open();
        }

        private void OnLogin(object sender)
        {
            _connection.Status = "online";
            _disco.DiscoverItems(new Jid(_connection.Server), DiscoverCallback);
        }

        private void DiscoverCallback(object sender, IQ iq, object data)
        {
            if (iq.Type == IqType.result)
            {
                Element query = iq.Query;
                if (query != null && query.GetType() == typeof(DiscoItems))
                {
                    DiscoItems items = query as DiscoItems;
                    DiscoItem[] itms = items.GetDiscoItems();

                    foreach (DiscoItem itm in itms)
                    {
                        if (itm.Jid != null)
                            _disco.DiscoverInformation(itm.Jid, new IqCB(DiscoverCallback), itm);
                    }
                }
            }
        }

        private void OnMessage(object sender, Message msg)
        {
            throw new NotImplementedException();
        }

        private void OnPresence(object sender, Presence pres)
        {
            if (pres.Type == PresenceType.subscribe)
            {
                if(MessageBox.Show("Allow connection from " + pres.From + "?", "Authorization request", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)== DialogResult.Yes)
                {
                    PresenceManager pm = new PresenceManager(_connection);
                    pm.ApproveSubscriptionRequest(pres.From);
                    Message message = new Message(pres.From, "Hello");
                    message.Type = MessageType.chat;
                    _connection.Send(message);
                }
            }
        }

        private void OnRosterEnd(object sender)
        {
            // todo: process _rosterList
            foreach (var item in _rosterList.Except(_tmpRosterList))
            {
                InvokeClientRemoved(new ClientEventArgs(){Client = new XmppClient(item)});
            }

            foreach (var item in _tmpRosterList.Except(_rosterList))
            {
                InvokeClientAdded(new ClientEventArgs(){Client = new XmppClient(item)});
            }

            _rosterList = _tmpRosterList;
        }

        private void OnRosterItem(object sender, RosterItem item)
        {
            switch (item.Subscription)
            {
                case SubscriptionType.remove:
                    //nothing to do:
                    break;
                default:
                    _tmpRosterList.Add(item);
                    break;
            }
        }

        private void OnRosterStart(object sender)
        {
            _tmpRosterList = new List<RosterItem>();
        }

        public event EventHandler<ClientEventArgs> ClientAdded;

        private void InvokeClientAdded(ClientEventArgs e)
        {
            EventHandler<ClientEventArgs> handler = ClientAdded;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ClientEventArgs> ClientRemoved;
        public string Name
        {
            get {
                return "XMPP"; }
        }

        private void InvokeClientRemoved(ClientEventArgs e)
        {
            EventHandler<ClientEventArgs> handler = ClientRemoved;
            if (handler != null) handler(this, e);
        }
    }
}
