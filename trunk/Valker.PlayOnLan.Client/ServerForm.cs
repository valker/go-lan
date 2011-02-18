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
            var serverConnector = transport.CreateMessageConnector("server");
            var clientConnector = transport.CreateMessageConnector("client");
            _server.AddConnector(serverConnector);
            var xmppConnector = new XmppTransport.XmppTransportImpl("xmpp");
            var client = new ClientImpl(this.textBox1.Text, new []{clientConnector, xmppConnector});
            var form = new MainForm(client);
            form.Show(this);
        }
    }
}
