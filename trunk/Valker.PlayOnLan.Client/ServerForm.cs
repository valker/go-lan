using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Valker.PlayOnLan.Client.Communication;
using Valker.PlayOnLan.Server;

namespace Valker.PlayOnLan.Client
{
    public partial class ServerForm : Form
    {
        private ServerImpl _server;

        public ServerForm(Server.ServerImpl server)
        {
            _server = server;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var transport = new LocalTransport();
            _server.AddConnector(transport.ServerConnector);
            var client = new ClientImpl(this.textBox1.Text, this, new[] { transport.ClientConnector });
            client.AcceptedPlayer += new EventHandler<AcceptedPlayerEventArgs>(client_AcceptedPlayer);
            client.RegisterNewPlayer();
        }

        void client_AcceptedPlayer(object sender, AcceptedPlayerEventArgs e)
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
