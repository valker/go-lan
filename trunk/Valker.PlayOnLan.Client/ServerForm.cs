using System;
using System.Windows.Forms;

namespace Valker.PlayOnLan.Client
{
    public sealed partial class ServerForm : Form
    {
        public event EventHandler<NewAgentCreatingEventArgs> NewAgentCreating = delegate { };

        public ServerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewAgentCreating(this, new NewAgentCreatingEventArgs() {Name = textBox1.Text});
        }
    }
}
