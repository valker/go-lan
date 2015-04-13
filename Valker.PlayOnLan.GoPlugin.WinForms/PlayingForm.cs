using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Goban;
using Valker.PlayOnLan.GoPlugin.Abstract;

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

        public Dictionary<string, Stone> Players { get; private set; }
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
            else
            {
                action();
            }
        }

        public string Gui
        {
            get { throw new NotImplementedException(); }
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
            string value;
            if (!args.TryGetValue("width", out value))
                throw new InvalidOperationException("parameters don't contain 'width'");
            var width = System.Convert.ToInt32(value, CultureInfo.InvariantCulture);

            var x = new[]
            {
                new {Name = "player1", Stone = Stone.Black},
                new {Name = "player2", Stone = Stone.White}
            };

            Players = x.Select(arg =>
            {
                string v;
                if (!args.TryGetValue(arg.Name, out v))
                {
                    throw new InvalidOperationException(string.Format("parameters don't contain '{0}'", arg.Name));
                }
                return Tuple.Create(v, arg.Stone);
            }).ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

            RunInUiThread(delegate { gobanControl1.N = width; });
        }

        private void ClientOnMark(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ClientOnShowMessage(object sender, ShowMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FieldChanged(object sender, CellChangedEventArgs e)
        {
            var x = e.Coordinates.GetCoordinate(0);
            var y = e.Coordinates.GetCoordinate(1);
            var stone = Convert(e.Cell);
            RunInUiThread(delegate { gobanControl1.SetStone(x, y, stone, true); });
        }

        private Stone Convert(ICell cell)
        {
            if (cell is EmptyCell) return Stone.None;
            var playerCell = cell as PlayerCell;
            if (playerCell != null)
            {
                return Players[playerCell.Player.PlayerName];
            }
            throw new InvalidOperationException(string.Format("unknown type of cell:{0}", cell.GetType()));
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

        private void btnPass_Click(object sender, EventArgs e)
        {
            Client.Pass();
        }
    }
}