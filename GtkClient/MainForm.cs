using System;
using Valker.PlayOnLan.Client2008.Communication;
using Valker.PlayOnLan.Api.Game;
using Gtk;
namespace GtkClient
{
	public partial class MainForm : Gtk.Window, IMainForm
	{
		private readonly Valker.PlayOnLan.Client2008.MainForm _mf;
 
		public MainForm (ClientImpl client) : base(Gtk.WindowType.Toplevel)
		{
			_mf = new Valker.PlayOnLan.Client2008.MainForm(client, this);
			this.Build ();
		}
	

		#region IMainForm implementation

		#endregion

		#region IForm implementation
		public void Show (IForm parent)
		{
			this.Parent = (Widget)parent;
			ShowAll();
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

