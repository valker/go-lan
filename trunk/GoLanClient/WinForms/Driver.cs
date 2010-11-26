using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GoLanClient.Engine;
using GoLanClient.UI;

namespace GoLanClient.WinForms
{
    class Driver
    {
        private IEngine _engine;

        public Driver(IEngine engine)
        {
            _engine = engine;
        }

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm(_engine));
        }
    }
}
