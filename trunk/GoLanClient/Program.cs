using System;
using System.Linq;
using System.Windows.Forms;
using GoLanClient.Engine;
using GoLanClient.UI;

namespace GoLanClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IEngine engine = new GoLanEngine();
            engine.Rules.Add(new Go.Rules());
            engine.Rules.Add(new OtherGame.Rules());
            Application.Run(new MainForm(engine));
        }
    }
}
