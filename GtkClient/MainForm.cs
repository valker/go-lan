using System;
using Valker.PlayOnLan.Client2008.Communication;
using Valker.PlayOnLan.Api.Game;
using Gtk;
using Valker.PlayOnLan.Client2008;
using System.Diagnostics;
namespace GtkClient
{
	public partial class MainForm : Gtk.Window, IMainForm
	{
		private readonly Valker.PlayOnLan.Client2008.MainForm _mf;
 
		public void OnSetPlayerName(object sender, SetPlayerNameEventArgs e)
		{
			this.Title = "PlayOnLan - " + e.Name;
		}
		
		public void OnGetSelectedGameInfo(object sender, GetSelectedGameInfoEventArgs e)
		{
			throw new NotImplementedException();
		}
		
		public void OnGetSelectedPartyInfo(object sender, GetSelectedPartyInfoEventArgs e)
		{
			throw new NotImplementedException();
		}
		public void OnUpdateGameInfo(object sender, UpdateGameInfoEventArgs e)
		{
			// TODO: should be implemented
			Debug.WriteLine(e.GameInfos.Length.ToString());
		}
		public void OnUpdatePartyStatesView(object sender, EventArgs e)
		{
			//todo: should be implemented
		}
		public MainForm (ClientImpl client) : base(Gtk.WindowType.Toplevel)
		{
			_mf = new Valker.PlayOnLan.Client2008.MainForm(client, this);
			this.Build ();
			
			            _mf.OnSetPlayerName += OnSetPlayerName;
            _mf.OnGetSelectedGameInfo += OnGetSelectedGameInfo;
            _mf.OnGetSelectedPartyInfo += OnGetSelectedPartyInfo;
            _mf.OnUpdateGameInfo += OnUpdateGameInfo;
            _mf.OnUpdatePartyStatesView += OnUpdatePartyStatesView;

            
            _mf.OnLoad();
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

