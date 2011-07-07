using System;
using System.Threading;
using Valker.PlayOnLan.Server2008;
using Valker.PlayOnLan.XmppTransport;

namespace ServerRunner
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("parameter missed.");
                return;
            }

            var transport = new XmppTransportImpl(args[0]) { ConnectorName = args[0] };
            var server = new ServerImpl(new []{transport});
            var ev = new AutoResetEvent(false);
            server.Closed += delegate { ev.Set(); };
            ev.WaitOne();
        }
    }
}
