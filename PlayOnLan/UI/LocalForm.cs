using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Valker.Api;
using Valker.PlayOnLan.Transport;

namespace Valker.PlayOnLan.UI
{
    public partial class LocalForm : Form
    {
        private LocalTransport _transport;

        public LocalForm(LocalTransport transport)
        {
            _transport = transport;
            _transport.ClientAdded += TransportOnClientAdded;
            _transport.ClientRemoved += TransportOnClientRemoved;
            InitializeComponent();
        }

        private void TransportOnClientRemoved(object sender, ClientEventArgs args)
        {
            LocalClientForm form = ((LocalClientForm)args.Client);
            if (!form.IsClosing)
            {
                form.Close();
            }
            listView1.Items.RemoveByKey(args.Client.Name);
        }

        private void TransportOnClientAdded(object sender, ClientEventArgs args)
        {
            var item = listView1.Items.Add(args.Client.Name);
            item.Name = args.Client.Name;
            ((Form)args.Client).Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            if (name.Length == 0)
            {
                return;
            }

            _transport.AddClient(name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                _transport.RemoveClient(_transport.FindClient(listView1.SelectedItems[0].Text));
            }
        }
    }
}
