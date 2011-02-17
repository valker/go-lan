using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Valker.PlayOnLan.Client.Communication;

namespace Valker.PlayOnLan.Client
{
    public partial class MainForm : Form
    {
        private ClientImpl _client;

        public MainForm(ClientImpl client)
        {
            _client = client;
            _client.SupportedGamesChanged +=  ClientOnSupportedGamesChanged;
            InitializeComponent();
        }

        private void ClientOnSupportedGamesChanged(object sender, SupportedGamesChangedEventArgs args)
        {
            listBox1.DataSource = args.Games;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _client.RetrieveSupportedGames();
        }
    }
}
