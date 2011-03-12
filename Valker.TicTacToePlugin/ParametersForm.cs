using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;

namespace Valker.TicTacToePlugin
{
    public partial class ParametersForm : Form
    {
        public IGameParameters Parameters
        {
            get { return new TicTacToeParameters(Convert.ToInt32(txtStones.Text), Convert.ToInt32(txtWidth.Text)); }
        }

        public ParametersForm()
        {
            InitializeComponent();
        }

    }
}
