using System;
using System.Windows.Forms;

namespace Valker.TicTacToePlugin
{
    public partial class PlayingForm : Form
    {
        public PlayingForm(int n, TicTacToeClient client)
        {
            InitializeComponent();
            goban.N = n;
            Client = client;
            Client.AllowMove += Client_AllowMove;
            Client.Wait += ClientOnWait;
        }

        private void ClientOnWait(object sender, EventArgs args)
        {
            toolStripStatusLabel1.Text = "Wait";
            goban.ClickedOnBoard -= Goban1OnClickedOnBoard;
        }

        private void Goban1OnClickedOnBoard(object sender, MouseEventArgs args)
        {
            Client.ClickedOnBoard(args);
        }

        void Client_AllowMove(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Make your move";
            goban.ClickedOnBoard += Goban1OnClickedOnBoard;
        }

        public TicTacToeClient Client { get; set; }
    }
}
