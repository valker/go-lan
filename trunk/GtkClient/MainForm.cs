using System;
namespace GtkClient
{
	public partial class MainForm : Gtk.Window
	{
		public MainForm () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

