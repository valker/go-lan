using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin.WinForms
{
    public partial class PlayingForm : Form, IPlayingForm
    {
        public PlayingForm(GoClient client)
        {
            InitializeComponent();

            Client = client;

            Client.Allow += Client_AllowMove;
            Client.Wait += ClientOnWait;
            Client.FieldChanged += FieldChanged;
            Client.ShowMessage += ClientOnShowMessage;
            Client.Mark += ClientOnMark;
            Client.Params += ClientOnParams;
            Client.Eated += ClientOnEated;
        }

        private void ClientOnEated(object sender, EatedEventArgs args)
        {
            RunInUiThread(delegate
                              {
                                  lblBlack.Text = args.Eated[0].ToString();
                                  lblWhite.Text = args.Eated[1].ToString();
                              });
        }

        private void ClientOnParams(object sender, ParamsEventArgs args)
        {
            if (!args.Keys.Contains("width")) {
                throw new InvalidOperationException("parameters don't contain width"); 
            }

            int width = System.Convert.ToInt32(args["width"], CultureInfo.InvariantCulture);
            RunInUiThread(delegate
                              {
                                  gobanControl1.N = width;
                              });
        }

        private void ClientOnMark(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ClientOnShowMessage(object sender, ShowMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FieldChanged(object sender, FieldChangedEventArgs e)
        {
            RunInUiThread(delegate
                              {
                                  gobanControl1.SetStone(e.X, e.Y, Convert(e.Stone), true);
                              });
        }

        private Valker.PlayOnLan.Goban.Stone Convert(Valker.PlayOnLan.Api.Stone stone)
        {
            switch (stone)
            {
                    case Stone.Black:
                    return Goban.Stone.Black;
                    case Stone.White:
                    return Goban.Stone.White;
                    case Stone.None:
                    return Goban.Stone.None;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void ClientOnWait(object sender, EventArgs e)
        {
            RunInUiThread(delegate
                              {
                                  gobanControl1.ClickedOnBoard -= GobanControl1OnClickedOnBoard;
                                  btnPass.Enabled = false;
                              });
        }

        private void Client_AllowMove(object sender, EventArgs e)
        {
            RunInUiThread(delegate
                              {
                                  gobanControl1.ClickedOnBoard += GobanControl1OnClickedOnBoard;
                                  btnPass.Enabled = true;
                              });
        }

        private void GobanControl1OnClickedOnBoard(object sender, MouseEventArgs args)
        {
            Client.Click(args.X, args.Y);
        }

        protected GoClient Client { get; set; }

        public void Show(IForm parent)
        {
            Show((IWin32Window) parent);
        }

        public void RunInUiThread(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else {
                action();
            }
        }

        public string Gui
        {
            get { throw new NotImplementedException(); }
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            Client.Pass();
        }
    }
}
