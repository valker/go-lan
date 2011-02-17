using System;
using System.Linq;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Client.Communication;

namespace Valker.PlayOnLan.Client
{
    public partial class MainForm : Form
    {
        private ClientImpl _client;

        public MainForm(ClientImpl client)
        {
            this._client = client;
            this._client.SupportedGamesChanged += this.ClientOnSupportedGamesChanged;
            this.InitializeComponent();
        }

        private void ClientOnSupportedGamesChanged(object sender, SupportedGamesChangedEventArgs args)
        {
            this.listBox1.DataSource =
                args.Games.Select(s => new GameInfo(s, (IMessageConnector) args.Sender)).ToArray();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this._client.RetrieveSupportedGames();
        }
    }
}