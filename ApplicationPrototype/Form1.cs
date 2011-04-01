using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApplicationPrototype
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabPage2.Parent == null)
            {
                tabControl1.TabPages.Add(tabPage2);
            }
        }
    }
}
