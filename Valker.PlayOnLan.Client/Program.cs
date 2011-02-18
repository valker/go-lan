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
            IMessageConnector xmppServer = new XmppTransportImpl("Xmpp server");

            // local server
            var server = new Server.ServerImpl(new[]{xmppServer});

            Form form = new ServerForm(server);
            Application.Run(form);
        }
    }
}