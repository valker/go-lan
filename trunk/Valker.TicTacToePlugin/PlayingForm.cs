using System;
using System.Windows.Forms;
using Valker.PlayOnLan.Goban;

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
            Client.FieldChanged += FieldChanged;
            Client.ShowMessage += ClientOnShowMessage;
        }

        private void ClientOnShowMessage(object sender, ShowMessageEventArgs args)
        {
            Parent.BeginInvoke(new Action(delegate { MessageBox.Show(this, args.Text, Text);  }));
        }

        void FieldChanged(object sender, FieldChangedEventArgs e)
        {
            goban.SetStone(e.X, e.Y, Convert2(e.StoneState), true);
        }

        private Stone Convert2(PlayOnLan.Api.Stone stone)
        {
            switch (stone)
            {
                    case PlayOnLan.Api.Stone.Black: return Stone.Black;
                    case PlayOnLan.Api.Stone.None: return Stone.None;
                    case PlayOnLan.Api.Stone.White:return Stone.White;
            }
            throw new ArgumentOutOfRangeException();
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
