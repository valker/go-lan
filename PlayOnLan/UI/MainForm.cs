using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GoLanClient.Engine;

namespace Valker.PlayOnLan.UI
{
    public partial class MainForm : Form
    {
        public MainForm(IEngine engine)
        {
            InitializeComponent();
        }
    }
}
