using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Client.Communication;
using Valker.PlayOnLan.Server;

namespace Valker.PlayOnLan.Client
{
    public partial class MainForm : Form
    {
        private ClientImpl _client;
        private Dictionary<string, string> _gameNames = new Dictionary<string, string>();
        private List<PartyInfo> _partyStates = new List<PartyInfo>();

        public MainForm(ClientImpl client)
        {
            _client = client;
            _client.SupportedGamesChanged += ClientOnSupportedGamesChanged;
            _client.PartyStatesChanged += ClientOnPartyStatesChanged;
            InitializeComponent();
        }

        private void ClientOnPartyStatesChanged(object sender, PartyStatesArgs args)
        {
            UpdatePartyStatesData(args);
            UpdatePartyStatesView();
        }

        private void UpdatePartyStatesView()
        {
            listView1.Invoke(new Action(delegate { listView1.Items.Clear(); }));
            foreach (PartyInfo info in _partyStates)
            {
                var item =
                    new ListViewItem(new[]
                                         {
                                             info.Name, _gameNames[info.GameTypeId],
                                             info.Status.ToString()
                                         }) {Tag = info};

                listView1.Invoke(new Action(() => listView1.Items.Add(item)));
            }
        }

        private void UpdatePartyStatesData(PartyStatesArgs args)
        {
            _partyStates.Clear();

            foreach (PartyState partyState in args.States)
            {
                _partyStates.Add(CreatePartyInfo(partyState, args.Connector));
            }
        }

        private static PartyInfo CreatePartyInfo(PartyState state, IMessageConnector connector)
        {
            var value = new PartyInfo()
                            {
                                Connector = connector,
                                GameTypeId = state.GameTypeId,
                                Name = state.Names[0],
                                Status = state.Status,
                                PartyId = state.PartyId
                            };
            return value;
        }


        private void ClientOnSupportedGamesChanged(object sender, SupportedGamesChangedEventArgs args)
        {
            if (_gameNames.Count == 0)
            {
                GameInfo[] gameInfos =
                    args.Games.Select(s => new GameInfo(s, (IMessageConnector) args.Sender)).ToArray();
                foreach (GameInfo info in gameInfos)
                {
                    _gameNames.Add(info.GameTypeId, info.ToString());
                }
                listBox1.Invoke(new Action(delegate { listBox1.DataSource = gameInfos; }));
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = _client.Name;
            _client.RetrieveSupportedGames();
        }

        private void Register(object sender, EventArgs e)
        {
            _client.RegisterNewParty((GameInfo) listBox1.SelectedItem, this);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _client.Dispose();
        }

        private void Accept(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;
            _client.AcceptParty((PartyInfo) listView1.SelectedItems[0].Tag);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _client.RetrieveSupportedGames();
        }
    }
}