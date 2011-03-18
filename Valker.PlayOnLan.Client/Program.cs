using System.Threading;
using System.Windows.Forms;
using Valker.PlayOnLan.Client.Communication;
using Valker.PlayOnLan.Client2008;
using Valker.PlayOnLan.Server;
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

            var form = new LoginForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var clientName = form.txtClientName.Text;
                var serverName = form.txtServerName.Text;
                var server = new XmppTransportImpl(clientName) {ConnectorName = serverName};
                var client = new ClientImpl(clientName, null,
                                            new[]
                                                {
                                                    new AgentInfo {ClientConnector = server, ClientIdentifier = serverName}
                                                });

                var ev = new AutoResetEvent(false);
                client.AcceptedPlayer += delegate { ev.Set(); };

                client.RegisterNewPlayer();
                ev.WaitOne();
                Form form2 = new MainForm(client);
                client.Parent = form2;
                // test form for local servers
                //Form form = new ServerForm(server);
                Application.Run(form2);
            }
        }
    }
}