using System.Threading;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Communication;
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
            var server = new XmppTransportImpl("client@mosdb9vf4j");
            Form form = new MainForm(new ClientImpl("client@mosdb9vf4j", null, new[] { server }));
            // test form for local servers
            //Form form = new ServerForm(server);
            Application.Run(form);
        }
    }
}