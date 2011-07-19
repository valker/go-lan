using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Jid _my;
        private XmppClientConnection _connection;
        private static readonly Encoding MyEncoding;
        List<string> _followers = new List<string>();

        public XmppTransportImpl(string name)
        {
            _my = new Jid(name);
            _connection = new XmppClientConnection(_my.Server);
            var ev = new AutoResetEvent(false);
            _connection.OnLogin += (sender => ev.Set());
            _connection.OnMessage += ConnectionOnOnMessage;
            _connection.OnPresence += ConnectionOnOnPresence;
            _connection.OnRosterItem += ConnectionOnOnRosterItem;
            const string password = "1";
            _connection.AutoAgents = false;
            _connection.AutoPresence = true;
            _connection.AutoRoster = true;
            _connection.AutoResolveConnectServer = true;
            _connection.Open(_my.User, password);
            ev.WaitOne();
            Name = name;
        }

        private void ConnectionOnOnRosterItem(object sender, RosterItem item)
        {
            Console.WriteLine(item.ToString());
        }

        private void ConnectionOnOnPresence(object sender, Presence presence)
        {
            Console.WriteLine(presence.ToString());
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

        public void FollowClient(string identifier)
        {
            Console.WriteLine("Follower added: " + identifier);
            _followers.Add(identifier);
            //_connection.RosterManager.AddRosterItem(new Jid(identifier));
            // TODO: implement subscribing to notification of client disconnected
            var jid = new Jid(identifier);
            _connection.PresenceManager.Subscribe(jid);
        }

        public event EventHandler<DisconnectedClientEventArgs> DisconnectedClient;

        private void InvokeDisconnectedClient(DisconnectedClientEventArgs e)
        {
            EventHandler<DisconnectedClientEventArgs> handler = DisconnectedClient;
            if (handler != null) handler(this, e);
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
    }
}