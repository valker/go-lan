using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using Valker.PlayOnLan.Server.Messages.Server;
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

            try
            {
                var transport = new XmppTransportImpl(args[0]) { ConnectorName = args[0] };
                var server = new ServerImpl(new[] { transport });
                var ev = new AutoResetEvent(false);
                server.Closed += delegate { ev.Set(); };
                ev.WaitOne();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception occurred: {0}", ex);
            }
        }
    }
}
