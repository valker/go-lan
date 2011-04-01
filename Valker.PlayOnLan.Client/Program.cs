using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Client.Communication;
using Valker.PlayOnLan.Client2008;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.XmppTransport;

namespace Valker.PlayOnLan.Client
{
    internal class Program
    {
        private static ServerForm _serverForm;

        private static void Main(string[] args)
        {
            if (args.FirstOrDefault(s => s == "local") != null)
            {
                Local();
            }
            else if (args.FirstOrDefault(s => s == "xmpp") != null)
            {
                Xmpp();
            }
        }

        private static void Local()
        {
            var server = new ServerImpl(new LocalMessageConnector[0]);
            _serverForm = new ServerForm();
            _serverForm.NewAgentCreating += delegate(object sender, NewAgentCreatingEventArgs args)
                                                {
                                                    var transport = new LocalTransport();
                                                    server.AddConnector(transport.ServerConnector);
                                                    ClientImpl client = CreateClientClient(transport, args.Name);
                                                    client.AcceptedPlayer += AcceptedPlayer;
                                                    client.RegisterNewPlayer();
                                                };
            Application.Run(_serverForm);
        }

        private static ClientImpl CreateClientClient(LocalTransport transport, string name)
        {
            return new ClientImpl(name, _serverForm,
                                  new[]
                                      {
                                          new AgentInfo
                                              {ClientConnector = transport.ClientConnector, ClientIdentifier = name}
                                      });
        }

        private static void AcceptedPlayer(object sender, AcceptedPlayerEventArgs e)
        {
            var client = (ClientImpl) sender;

            if (e.Status)
            {
                var form = new MainForm(client);
                form.Show(_serverForm);
            }
            else
            {
                client.Dispose();
            }
        }

        private static void Xmpp()
        {
            var form = new LoginForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                string clientName = form.txtClientName.Text;
                string serverName = form.txtServerName.Text;
                IMessageConnector server = new XmppTransportImpl(clientName) {ConnectorName = serverName};
                var client = new ClientImpl(clientName, null,
                                            new[]
                                                {
                                                    new AgentInfo
                                                        {ClientConnector = server, ClientIdentifier = serverName}
                                                });

                var ev = new AutoResetEvent(false);
                client.AcceptedPlayer += delegate { ev.Set(); };
                client.RegisterNewPlayer();
                ev.WaitOne();
                Form form2 = new MainForm(client);
                client.Parent = form2;
                Application.Run(form2);
            }
        }
    }
}