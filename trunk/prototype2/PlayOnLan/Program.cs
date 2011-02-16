using System;
using System.Diagnostics;
using System.Windows.Forms;
using Valker.Api;
using Valker.PlayOnLan.Transport;
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

            IEngine engine = new Engine.Engine();
            //engine.AddTransport(new XmppTransport());
            var mainForm = new MainForm(engine);
            engine.AddTransport(new LocalTransport(mainForm));
            engine.OnMessage += EngineOnOnMessage;
            Application.Run(mainForm);
        }

        private static void EngineOnOnMessage(object sender, OnMessageEventArgs args)
        {
            Debug.WriteLine(args.Message);
        }
    }
}