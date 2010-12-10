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
        IEngine _engine;

        public MainForm(IEngine engine)
        {
            _engine = engine;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _engine.NeiboursChanged += new EventHandler<NeiboursChangedEventArgs>(_engine_NeiboursChanged);
            _engine.Start();
        }

        void _engine_NeiboursChanged(object sender, NeiboursChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<NeiboursChangedEventArgs>(_engine_NeiboursChanged), new object[] { sender, e });
            }
            else
            {
                if (e.Added)
                {
                    var item = listView1.Items.Add(e.Neibour.Name);
                    item.Name = e.Neibour.Name;
                }
                else
                {
                    listView1.Items.RemoveByKey(e.Neibour.Name);
                }
            }
        }
    }
}
