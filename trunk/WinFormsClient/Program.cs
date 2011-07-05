using System;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Client2008;
using Valker.PlayOnLan.Client2008.Communication;

namespace WinFormsClient
{
    class Program : Valker.PlayOnLan.Client2008.Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Program().MainImpl(args);
        }

        protected override void Run(IForm form)
        {
            Application.Run((Form) form);
        }

        protected override IServerForm CreateServerForm()
        {
            return new ServerForm();
        }

        protected override IMainForm CreateMainForm(ClientImpl client)
        {
            return new MainForm(client);
        }

        protected override AuthentificationParams GetAuthParams()
        {
            var param = new AuthentificationParams();
            param.Name = "player@acer";
            param.ServerName = "server@acer";
            return param;
        }
    }
}
