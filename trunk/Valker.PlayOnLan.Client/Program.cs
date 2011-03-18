using System.Threading;
using System.Windows.Forms;
using Valker.PlayOnLan.Client.Communication;
using Valker.PlayOnLan.XmppTransport;

namespace Valker.PlayOnLan.Client
{
    internal class Program
    {
        private static void Main()
        {
            // local server
            //var server = new Server.ServerImpl(new IMessageConnector[0]);
            //Thread.Sleep(10000);
            var server = new XmppTransportImpl("client@mosdb9vf4j");
            var client = new ClientImpl("client@mosdb9vf4j", null, new[] { server });

            var ev = new AutoResetEvent(false);
            client.AcceptedPlayer += delegate { ev.Set(); };

            client.RegisterNewPlayer();
            ev.WaitOne();
            Form form = new MainForm(client);
            // test form for local servers
            //Form form = new ServerForm(server);
            Application.Run(form);
        }
    }
}