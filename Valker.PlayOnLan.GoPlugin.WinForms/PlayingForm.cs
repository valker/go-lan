using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin.WinForms
{
    public partial class PlayingForm : Form, IPlayingForm
    {
        public PlayingForm()
        {
            InitializeComponent();
        }

        public void Show(IForm parent)
        {
            Show((IWin32Window) parent);
        }

        public void RunInUiThread(Action action)
        {
            throw new NotImplementedException();
        }

        public string Gui
        {
            get { throw new NotImplementedException(); }
        }
    }
}
