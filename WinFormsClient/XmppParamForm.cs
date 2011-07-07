using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsClient
{
    public partial class XmppParamForm : Form
    {
        public XmppParamForm()
        {
            InitializeComponent();
        }

        public string UserAccount
        {
            get { return txtUserAccount.Text; }
        }

        public string ServerAccount
        {
            get { return txtServerAccount.Text; }
        }
    }
}
