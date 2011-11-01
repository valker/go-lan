using System;
using Gtk;
using System.Linq;
using Valker.PlayOnLan.Server;
using Valker.PlayOnLan.Client2008;

namespace GtkClient
{
	class MainClass : Valker.PlayOnLan.Client2008.Program
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			new MainClass().MainImpl(args);

		}
		#region implemented abstract members of Valker.PlayOnLan.Client2008.Program
		protected override void Run (Valker.PlayOnLan.Api.Game.IForm form)
		{
			form.Show(null);
		}
		
		
		protected override Valker.PlayOnLan.Api.Game.IServerForm CreateServerForm ()
		{
			return new ServerWindow();
		}
		
		
		protected override Valker.PlayOnLan.Api.Game.IMainForm CreateMainForm (Valker.PlayOnLan.Client2008.Communication.ClientImpl client)
		{
			return new MainForm(client);
		}
		
		
		protected override Valker.PlayOnLan.Client2008.AuthentificationParams GetAuthParams ()
		{
			var ap = new AuthentificationParams();
			// todo: create a modal dialog and fill fields of 'ap'
			return ap;
		}
		
		#endregion
	}
}

