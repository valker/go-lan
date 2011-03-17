using System;
using System.Diagnostics;
using System.Threading;
using agsXMPP;
using agsXMPP.protocol.client;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.XmppTransport
{
    public class XmppTransportImpl : IMessageConnector
    {
        Jid _my;
        private XmppClientConnection _connection;

        public XmppTransportImpl(string name)
        {
            _my = new Jid(name);
            _connection = new XmppClientConnection(_my.Server);
            var ev = new AutoResetEvent(false);
            _connection.OnLogin += (sender => ev.Set());
            _connection.OnMessage += ConnectionOnOnMessage;
            const string password = "1";
            _connection.AutoAgents = false;
            _connection.AutoPresence = true;
            _connection.AutoRoster = true;
            _connection.AutoResolveConnectServer = true;
            _connection.Open(_my.User, password);
            ev.WaitOne();
            Name = name;
        }

        private void ConnectionOnOnMessage(object sender, Message message)
        {
            Trace.WriteLine("Message arrived");
            MessageArrived(this, new MessageEventArgs(message.From.Bare, _my.Bare, message.Body));
        }


        #region IMessageConnector Members

        public string Name { get; set; }

        #endregion

        #region Implementation of IMessageConnector

        public void Send(string message)
        {
            // TODO: implement send message
        }

        public string ConnectorName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void Send(object fromIdentifier, object toIdentifier, string message)
        {
            _connection.Send(new Message((string)toIdentifier, message));
        }

        public string[] Clients
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<MessageEventArgs> MessageArrived;
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