
// This file has been generated by the GUI designer. Do not modify.
namespace GtkClient
{
	public partial class ServerWindow
	{
		private global::Gtk.Fixed fixed1;

		private global::Gtk.Entry txtName;

		private global::Gtk.Button btnCreate;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget GtkClient.ServerWindow
			this.Name = "GtkClient.ServerWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("ServerWindow");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child GtkClient.ServerWindow.Gtk.Container+ContainerChild
			this.fixed1 = new global::Gtk.Fixed ();
			this.fixed1.Name = "fixed1";
			this.fixed1.HasWindow = false;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.txtName = new global::Gtk.Entry ();
			this.txtName.CanFocus = true;
			this.txtName.Name = "txtName";
			this.txtName.IsEditable = true;
			this.txtName.InvisibleChar = '●';
			this.fixed1.Add (this.txtName);
			global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.txtName]));
			w1.X = 8;
			w1.Y = 10;
			// Container child fixed1.Gtk.Fixed+FixedChild
			this.btnCreate = new global::Gtk.Button ();
			this.btnCreate.CanDefault = true;
			this.btnCreate.CanFocus = true;
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.UseUnderline = true;
			this.btnCreate.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");
			this.fixed1.Add (this.btnCreate);
			global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed1[this.btnCreate]));
			w2.X = 173;
			w2.Y = 9;
			this.Add (this.fixed1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.btnCreate.HasDefault = true;
			this.Show ();
			this.btnCreate.Clicked += new global::System.EventHandler (this.OnButton1Clicked);
		}
	}
}
