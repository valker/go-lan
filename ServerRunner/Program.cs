using System.Threading;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.XmppTransport;

namespace ServerRunner
{
    static class Program
    {
        static void Main(string[] args)
        {
            var transport = new XmppTransportImpl("server@mosdb9vf4j");
            var server = new ServerImpl(new []{transport});
            var ev = new AutoResetEvent(false);
            server.Closed += delegate { ev.Set(); };
            ev.WaitOne();
        }
    }
}
