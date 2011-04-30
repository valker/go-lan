using System;
using Gtk;
using System.Linq;
using Valker.PlayOnLan.Server;

namespace GtkClient
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/*
			Application.Init ();
			ServerForm win = new ServerForm ();
			win.Show ();
			Application.Run ();
			*/			
			if (args.FirstOrDefault (s => s == "local") != null) {
				Local ();
			} else if (args.FirstOrDefault (s => s == "xmpp") != null) {
				Xmpp ();
			}
			
		}
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
	}
}

