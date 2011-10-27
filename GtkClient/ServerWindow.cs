using System;
using Valker.PlayOnLan.Api.Game;
namespace GtkClient
{
	public partial class ServerWindow : Gtk.Window, IServerForm
	{
		public ServerWindow () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	

		#region IServerForm implementation
		public event EventHandler<NewAgentCreatingEventArgs> NewAgentCreating;
		#endregion

		#region IForm implementation
		public void Show (IForm parent)
		{
			throw new NotImplementedException ();
		}

		public void RunInUiThread (Action action)
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

