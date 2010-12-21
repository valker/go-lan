using System;
using System.Linq;
using System.Windows.Forms;
using Valker.Api;
using Valker.PlayOnLan.Engine;
using Valker.PlayOnLan.UI;

namespace Valker.PlayOnLan
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IEngine engine = new GoLanEngine();
            engine.OnMessage += EngineOnOnMessage;
            Application.Run(new MainForm(engine));
        }

        private static void EngineOnOnMessage(object sender, OnMessageEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}