using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;
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
            //Application.Run(new Form1());
        }

        protected override void Run(IForm form)
        {
            Application.Run((Form) form);
        }

        protected override IServerForm CreateServerForm()
        {
            return new ServerForm();
        }

        protected override IPlayingForm CreatePlayingForm(ClientImpl client)
        {
            throw new NotImplementedException();
        }

        protected override IMainForm CreateMainForm(ClientImpl client)
        {
            return new MainForm(client);
        }

        protected override AuthentificationParams GetAuthParams()
        {
            throw new NotImplementedException();
        }
    }
}
