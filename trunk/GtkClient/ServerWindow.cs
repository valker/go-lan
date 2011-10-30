using System;
using Valker.PlayOnLan.Api.Game;
using Gtk;

namespace GtkClient
{
	public partial class ServerWindow : Gtk.Window, IServerForm
	{
		public ServerWindow () : base(Gtk.WindowType.Toplevel)
		{
			Build ();
		}
		
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}


		#region IServerForm implementation
		public event EventHandler<NewAgentCreatingEventArgs> NewAgentCreating;
		#endregion

		#region IForm implementation
		public void Show (IForm parent)
		{
			this.ShowAll();
			Application.Run();
		}

		public void RunInUiThread (System.Action action)
		{
			throw new NotImplementedException ();
		}

		public string Gui {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
}
}

