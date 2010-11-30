using System;
using System.Linq;
using System.Windows.Forms;
using GoLanClient.Engine;
using Valker.PlayOnLan.UI;

namespace Valker.PlayOnLan
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
            Application.Run(new MainForm(engine));
        }
    }
}