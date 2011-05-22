using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;

namespace WinFormsClient
{
    public partial class ServerForm : Form, IServerForm
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        public void Show(IForm form)
        {
            throw new NotImplementedException();
        }

        public void RunInUiThread(Action action)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<NewAgentCreatingEventArgs> NewAgentCreating;

        private void InvokeNewAgentCreating(NewAgentCreatingEventArgs e)
        {
            EventHandler<NewAgentCreatingEventArgs> creating = NewAgentCreating;
            if (creating != null) creating(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InvokeNewAgentCreating(new NewAgentCreatingEventArgs(){Name = textBox1.Text});
        }
    }
}
