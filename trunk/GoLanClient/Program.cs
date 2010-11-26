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
            Application.Run(new Form1(engine));
        }
    }
}
