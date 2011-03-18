using System;
using System.Windows.Forms;
using Valker.PlayOnLan.Client.Communication;
using Valker.PlayOnLan.Server;

namespace Valker.PlayOnLan.Client
{
    public partial class ServerForm : Form
    {
        private ServerImpl _server;

        public ServerForm(ServerImpl server)
        {
            _server = server;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var transport = new LocalTransport();
            _server.AddConnector(transport.ServerConnector);
            var client = new ClientImpl(textBox1.Text, this,
                                        new[]
                                            {
                                                new AgentInfo
                                                    {ClientConnector = transport.ClientConnector, ClientIdentifier = ""}
                                            });
            client.AcceptedPlayer += AcceptedPlayer;
            client.RegisterNewPlayer();
        }

        void AcceptedPlayer(object sender, AcceptedPlayerEventArgs e)
        {
            var client = (ClientImpl)sender;

            if (e.Status)
            {
                var form = new MainForm(client);
                form.Show(this);
            }
            else
            {
                client.Dispose();
            }
        }
    }
}
