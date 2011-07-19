using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.XmppTransport;

namespace XmppTransportTest
{
    [TestFixture]
    public class TransportTest
    {
        [Test]
        public void TestSimpleMessage()
        {
            try
            {
                XmppTransportImpl sender = new XmppTransportImpl("player@acer");
                XmppTransportImpl receiver = new XmppTransportImpl("server@acer");
                AutoResetEvent ev = new AutoResetEvent(false);
                const string message = "<русский>\n<english>";
                string receivedMessage = "";
                receiver.MessageArrived += delegate(object o, MessageEventArgs args)
                                               {
                                                   receivedMessage = args.Message;
                                                   ev.Set();
                                               };
                sender.Send("player@acer", "server@acer", message);

                var timeout = new TimeSpan(0, 0, 10);
                Assert.IsTrue(ev.WaitOne(timeout), "Communications are not working");
                Assert.AreEqual(message, receivedMessage, "Received message is not equal to the sent. Transport breaks content!!!");

            }
            catch (SocketException ex)
            {
                Assert.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
