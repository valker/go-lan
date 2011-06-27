using System;
using System.Linq;
using System.Threading;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Client;
using Valker.PlayOnLan.Client.Communication;
using Valker.PlayOnLan.Client2008.Communication;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Server2008;
using Valker.PlayOnLan.XmppTransport;

namespace Valker.PlayOnLan.Client2008
{
    /// <summary>
    /// Base class for each clients application
    /// </summary>
    public abstract class Program
    {
        private IServerForm _serverForm;

        public void MainImpl(string[] args)
        {
            if (args == null) throw new ArgumentNullException("args");

            if (args.FirstOrDefault(s => s == "local") != null)
            {
                Local();
            }
            else if (args.FirstOrDefault(s => s == "xmpp") != null)
            {
                Xmpp();
            }
            else
            {
                Local();
            }
        }

        private void Local()
        {
            var server = new ServerImpl(new LocalMessageConnector[0]);
            _serverForm = CreateServerForm();
            _serverForm.NewAgentCreating += delegate(object sender, NewAgentCreatingEventArgs args)
                                                {
                                                    var transport = new LocalTransport();
                                                    server.AddConnector(transport.ServerConnector);
                                                    var client = CreateClientClient(transport, args.Name);
                                                    client.AcceptedPlayer += AcceptedPlayer;
                                                    client.RegisterNewPlayer();
                                                };
            Run(_serverForm);
        }

        protected abstract void Run(IForm form);

        protected abstract IServerForm CreateServerForm();

        private ClientImpl CreateClientClient(LocalTransport transport, string name)
        {
            return new ClientImpl(name, _serverForm,
                                  new[]
                                      {
                                          new AgentInfo
                                              {ClientConnector = transport.ClientConnector, ClientIdentifier = name}
                                      });
        }

        private void AcceptedPlayer(object sender, AcceptedPlayerEventArgs e)
        {
            var client = (ClientImpl) sender;

            if (e.Status)
            {
                IMainForm form = CreateMainForm(client);
                form.Show(_serverForm);
            }
            else
            {
                client.Dispose();
            }
        }

        private void Xmpp()
        {
            AuthentificationParams param = GetAuthParams();
            if (param != null)
            {
                string clientName = param.Name;
                string serverName = param.ServerName;
                var server = new XmppTransportImpl(clientName) {ConnectorName = serverName};
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
                IMainForm form2 = CreateMainForm(client);
                client.Parent = form2;
                Run(form2);
            }
        }

        protected abstract IMainForm CreateMainForm(ClientImpl client);

        protected abstract AuthentificationParams GetAuthParams();
    }
}