using System;
using Gtk;
using System.Linq;
using Valker.PlayOnLan.Server;

namespace GtkClient
{
	class MainClass : Valker.PlayOnLan.Client2008.Program
	{
		public static void Main (string[] args)
		{
			/*
			Application.Init ();
			ServerForm win = new ServerForm ();
			win.Show ();
			Application.Run ();
			*/			
			/*
			if (args.FirstOrDefault (s => s == "local") != null) {
				Local ();
			} else if (args.FirstOrDefault (s => s == "xmpp") != null) {
				Xmpp ();
			}
			*/
			new MainClass().MainImpl(args);

		}
#if false
		static void Local ()
		{
			var server = new ServerImpl(new LocalMessageConnector[0]);
			var serverForm = new ServerForm();
			serverForm.NewAgentCreating += delegate(object sender, EventArgs args) {
			};
			serverForm.Show();
			Application.Run();
		}

		static void Xmpp ()
		{
			throw new System.NotImplementedException ();
		}
#endif
		#region implemented abstract members of Valker.PlayOnLan.Client2008.Program
		protected override void Run (Valker.PlayOnLan.Api.Game.IForm form)
		{
			throw new System.NotImplementedException();
		}
		
		
		protected override Valker.PlayOnLan.Api.Game.IServerForm CreateServerForm ()
		{
			return new ServerWindow();
		}
		
		
		protected override Valker.PlayOnLan.Api.Game.IMainForm CreateMainForm (Valker.PlayOnLan.Client2008.Communication.ClientImpl client)
		{
			throw new System.NotImplementedException();
		}
		
		
		protected override Valker.PlayOnLan.Client2008.AuthentificationParams GetAuthParams ()
		{
			throw new System.NotImplementedException();
		}
		
		#endregion
	}
}

