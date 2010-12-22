using System;
using System.Windows.Forms;
using Valker.Api;

namespace Valker.PlayOnLan.UI
{
    public partial class MainForm : Form
    {
        IEngine _engine;

        public MainForm(IEngine engine)
        {
            _engine = engine;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var transport in _engine.Transports)
            {
                listView1.Items.Add(transport.Name); 
            }
            _engine.Start();
        }
    }
}
