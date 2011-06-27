using System;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Client2008;
using Valker.PlayOnLan.Client2008.Communication;
using Valker.PlayOnLan.Server;

namespace WinFormsClient
{
    public partial class MainForm : Form, IMainForm
    {
        private readonly Valker.PlayOnLan.Client2008.MainForm _mf;
        public MainForm(ClientImpl client)
        {
            _mf = new Valker.PlayOnLan.Client2008.MainForm(client, this);
            InitializeComponent();
        }

        private void OnUpdatePartyStatesView(object sender, EventArgs args)
        {
            Invoke(new Action(delegate
                                  {
                                      listBox2.Items.Clear();
                                      foreach (var partyInfo in _mf.GetPartyStates())
                                      {
                                          listBox2.Items.Add(partyInfo);
                                      }
                                  }));
        }

        private void OnUpdateGameInfo(object sender, UpdateGameInfoEventArgs args)
        {
            listBox1.DataSource = args.GameInfos;
        }

        private void OnGetSelectedPartyInfo(object sender, GetSelectedPartyInfoEventArgs args)
        {
            args.PartyInfo = (PartyInfo) listBox2.SelectedItem;
        }

        private void OnGetSelectedGameInfo(object sender, GetSelectedGameInfoEventArgs args)
        {
            args.GameInfo = (GameInfo) listBox1.SelectedItem;
        }

        private void OnSetPlayerName(object sender, SetPlayerNameEventArgs args)
        {
            Text = args.Name;
        }

        public void Show(IForm parent)
        {
            Show((IWin32Window) parent);
        }

        public void RunInUiThread(Action action)
        {
            throw new NotImplementedException();
        }

        public string Gui
        {
            get { return "winforms"; }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _mf.OnSetPlayerName += OnSetPlayerName;
            _mf.OnGetSelectedGameInfo += OnGetSelectedGameInfo;
            _mf.OnGetSelectedPartyInfo += OnGetSelectedPartyInfo;
            _mf.OnUpdateGameInfo += OnUpdateGameInfo;
            _mf.OnUpdatePartyStatesView += OnUpdatePartyStatesView;

            
            _mf.OnLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mf.Register();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mf.OnSetPlayerName -= OnSetPlayerName;
            _mf.OnGetSelectedGameInfo -= OnGetSelectedGameInfo;
            _mf.OnGetSelectedPartyInfo -= OnGetSelectedPartyInfo;
            _mf.OnUpdateGameInfo -= OnUpdateGameInfo;
            _mf.OnUpdatePartyStatesView -= OnUpdatePartyStatesView;

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            _mf.Accept();
        }
    }
}
