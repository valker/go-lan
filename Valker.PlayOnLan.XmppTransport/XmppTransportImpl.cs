using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq.roster;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.XmppTransport
{
    public class XmppTransportImpl : IMessageConnector
    {
        readonly Jid _my;
        private readonly XmppClientConnection _connection;
        private static readonly Encoding MyEncoding;
        readonly List<object> _followers = new List<object>();
        private readonly List<string> _allowSubscribtionFrom = new List<string>();
        private Exception _exception;

        public XmppTransportImpl(string name)
        {
            _my = new Jid(name);
            _connection = new XmppClientConnection(_my.Server);
            var ev = new AutoResetEvent(false);
            _connection.OnLogin += (sender => ev.Set());
            _connection.OnMessage += ConnectionOnOnMessage;
            _connection.OnPresence += ConnectionOnOnPresence;
            _connection.OnRosterStart += ConnectionOnOnRosterStart;
            _connection.OnRosterEnd +=ConnectionOnOnRosterEnd;
            _connection.OnRosterItem += ConnectionOnOnRosterItem;

            const string password = "1";

            _connection.AutoAgents = false;
            _connection.AutoPresence = true;
            _connection.AutoRoster = true;
            _connection.AutoResolveConnectServer = true;
            _connection.Open(_my.User, password);
            _connection.OnSocketError+=delegate(object sender, Exception exception)
                                           {
                                               _exception = exception;
                                               ev.Set();
                                           };
            ev.WaitOne();
            if (_exception != null)
            {
                throw new XmppTransportException("XMPP Connection problem", _exception);
            }

            Name = name;
        }

        private void ConnectionOnOnRosterEnd(object sender)
        {
            Trace.WriteLine("ConnectionOnOnRosterEnd");
        }

        private void ConnectionOnOnRosterStart(object sender)
        {
            Trace.WriteLine("ConnectionOnOnRosterStart");
        }

        private void ConnectionOnOnRosterItem(object sender, RosterItem item)
        {
            Trace.WriteLine("ConnectionOnOnRosterItem:" + item.ToString());
        }

        private void ConnectionOnOnPresence(object sender, Presence presence)
        {
            switch (presence.Type)
            {
                case PresenceType.subscribe:
                    ProcessSubscribing(presence);
                    break;
                case PresenceType.unavailable:
                    ProcessUnavailable(presence);
                    break;
                default:
                    Trace.WriteLine(presence.ToString());
                    break;
            }
        }

        private void ProcessUnavailable(Presence presence)
        {
            var identifier = _followers.Find(s => (string) s == presence.From.Bare);
            if (identifier != null)
            {
                InvokeDisconnectedClient(new DisconnectedClientEventArgs(){Identifier = identifier});
            }
        }

        private void ProcessSubscribing(Presence presence)
        {
            if (_allowSubscribtionFrom.Contains(presence.From.Bare))
            {
                _connection.PresenceManager.ApproveSubscriptionRequest(presence.From);
            } 
            else
            {
                _connection.PresenceManager.RefuseSubscriptionRequest(presence.From);
            }
        }

        static XmppTransportImpl()
        {
            MyEncoding = Encoding.UTF7;
        }

        private void ConnectionOnOnMessage(object sender, Message message)
        {
            Trace.WriteLine("Message arrived");
            var fromIdentifier = message.From.Bare;
            var toIdentifier = _my.Bare;
            byte[] bytes = Encoding.ASCII.GetBytes(message.Body);
            string newMsg = MyEncoding.GetString(bytes);
            var args = new MessageEventArgs(fromIdentifier, toIdentifier, newMsg);
            MessageArrived(this, args);
        }


        #region IMessageConnector Members

        public string Name { get; set; }

        #endregion

        #region Implementation of IMessageConnector

        public string ConnectorName
        {
            get; set;
        }

        public void Send(object fromIdentifier, object toIdentifier, string message)
        {
            var bytes = MyEncoding.GetBytes(message);
            var str = Encoding.ASCII.GetString(bytes);
            _connection.Send(new Message((string)toIdentifier, str));
        }

        public void WatchOther(object identifier)
        {
            _followers.Add(identifier);
            var jid = new Jid(identifier.ToString());
            _connection.PresenceManager.Subscribe(jid);
        }

        public event EventHandler<DisconnectedClientEventArgs> DisconnectedOther;

        private void InvokeDisconnectedClient(DisconnectedClientEventArgs e)
        {
            DisconnectedOther?.Invoke(this, e);
        }

        public event EventHandler<MessageEventArgs> MessageArrived = delegate { };
        public event EventHandler Closed;

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            // TODO: to be implemented
            return;
        }

        #endregion

        public void AllowSubscibtionFrom(string jid)
        {
            _allowSubscribtionFrom.Add(jid);
        }
    }
}