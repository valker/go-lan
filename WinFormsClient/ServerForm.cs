﻿using System;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;

namespace WinFormsClient
{
    /// <summary>
    /// Allows connecting to local server
    /// </summary>
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
            Invoke(action);
        }

        public string Gui => "winforms";

        public event EventHandler<NewAgentCreatingEventArgs> NewAgentCreating;

        private void InvokeNewAgentCreating(NewAgentCreatingEventArgs e)
        {
            NewAgentCreating?.Invoke(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InvokeNewAgentCreating(new NewAgentCreatingEventArgs {Name = textBox1.Text});
        }
    }
}
