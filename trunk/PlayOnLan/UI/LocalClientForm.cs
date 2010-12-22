using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Valker.Api;

namespace Valker.PlayOnLan.UI
{
    public partial class LocalClientForm : Form, IClient
    {
        public LocalClientForm(string text)
        {
            InitializeComponent();
            Name = text;
            IsClosing = false;
        }

        private void LocalClientForm_Load(object sender, EventArgs e)
        {
            Text = "Local User : " + Name;
        }

        private void LocalClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        public bool IsClosing { get; private set; }

        private void LocalClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing = true;
        }
    }
}
