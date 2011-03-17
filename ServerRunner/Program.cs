using System.Threading;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.XmppTransport;

namespace ServerRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var transport = new XmppTransportImpl("xmpp");
            var server = new ServerImpl(new []{transport});
            var ev = new AutoResetEvent(false);
            server.Closed += delegate { ev.Set(); };
            ev.WaitOne();
        }
    }
}
