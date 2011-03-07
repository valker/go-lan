using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Api.Communication;

namespace Server.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateServer()
        {
            var server = new ServerImpl(new IMessageConnector[0]);
            Assert.IsNotNull(server);
        }

        class CO : IClientInfo
        {
            private MC mC;
            private object id = new object();

            public CO(IMessageConnector mC)
            {
                // TODO: Complete member initialization
                this.ClientConnector = mC;
            }
            public IMessageConnector ClientConnector
            {
                get; private set;
            }

            public object ClientIdentifier
            {
                get { return id; }
            }

            public bool Equals(IClientInfo other)
            {
                throw new NotImplementedException();
            }
        }
        class MC : IMessageConnector
        {

            public string ConnectorName
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public void Send(object fromIdentifier, object toIdentifier, string message)
            {
                //throw new NotImplementedException();
                if (fromIdentifier == null) throw new ArgumentNullException();
                if (toIdentifier == null) throw new ArgumentNullException();
                if (message == null) throw new ArgumentNullException();

            }

            public string[] Clients
            {
                get { throw new NotImplementedException(); }
            }

            public event EventHandler<MessageEventArgs> MessageArrived;

            public event EventHandler Closed;

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
        [TestMethod]
        public void AddPlayer()
        {
            var server = new ServerImpl(new IMessageConnector[0]);
            var client = new CO(new MC());
            server.RegisterNewPlayer(client, "PlayerName");
        }

        [TestMethod]
        public void AddPlayerWithSameName()
        {
            var server = new ServerImpl(new IMessageConnector[0]);
            var client = new CO(new MC());
            server.RegisterNewPlayer(client, "PlayerName");
            server.RegisterNewPlayer(client, "PlayerName");
        }
    }
}
