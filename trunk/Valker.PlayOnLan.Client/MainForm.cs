using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Client.Communication;
using Valker.PlayOnLan.Server;

namespace Valker.PlayOnLan.Client
{
    public partial class MainForm : Form
    {
        private ClientImpl _client;

        public MainForm(ClientImpl client)
        {
            this._client = client;
            this._client.SupportedGamesChanged += this.ClientOnSupportedGamesChanged;
            this._client.MessageToShow += this.ClientOnMessageToShow;
            this._client.PartyStatesChanged += this.ClientOnPartyStatesChanged;
            this.InitializeComponent();
        }

        Dictionary<GameIdentifier, PartyState> _partyStates = new Dictionary<GameIdentifier, PartyState>();

        private void ClientOnPartyStatesChanged(object sender, PartyStatesArgs args)
        {
            this.UpdatePartyStatesData(args);

            UpdatePartyStatesView();
        }

        Dictionary<string, string> _gameNames = new Dictionary<string, string>();

        private void UpdatePartyStatesView()
        {
            listView1.Invoke(new Action(delegate { listView1.Items.Clear(); }));
            foreach (KeyValuePair<GameIdentifier, PartyState> pair in this._partyStates)
            {
                listView1.Invoke(new Action(delegate {
                    var item = new ListViewItem(new string[] { pair.Key.Name, _gameNames[pair.Key.GameType], pair.Value.Status.ToString() });
                    listView1.Items.Add(item);
                }));
            }
        }

        private string GetPartyInfoString(GameIdentifier gameIdentifier, PartyState state)
        {
            return gameIdentifier.Name + " " + this.GetGameName(gameIdentifier.GameType) + state.Status;
        }

        private string GetGameName(string type)
        {
            return _gameNames[type];
        }

        private void UpdatePartyStatesData(PartyStatesArgs args)
        {
            var toRemove = new List<GameIdentifier>();
            foreach (var key in this._partyStates.Keys.Where(identifier => identifier.Connector.Equals(args.Connector)))
            {
                // this state is not actual
                toRemove.Add(key);
            }

            foreach (var gameIdentifier in toRemove)
            {
                this._partyStates.Remove(gameIdentifier);
            }

            foreach (var partyState in args.States)
            {
                this._partyStates.Add(GenerateKey(partyState, args.Connector), partyState);
            }
        }

        private static GameIdentifier GenerateKey(PartyState state, IMessageConnector connector)
        {
            var value = new GameIdentifier() {Connector = connector, GameType = state.GameTypeId, Name = state.playerNames[0]};
            return value;
        }


        private void ClientOnMessageToShow(object sender, MessageEventArgs args)
        {
            MessageBox.Show(args.Message);
        }

        private void ClientOnSupportedGamesChanged(object sender, SupportedGamesChangedEventArgs args)
        {
            if (_gameNames.Count == 0)
            {
                var gameInfos = args.Games.Select(s => new GameInfo(s, (IMessageConnector) args.Sender)).ToArray();
                foreach (GameInfo info in gameInfos)
                {
                    _gameNames.Add(info.GameId, info.ToString());
                }
                this.listBox1.DataSource = gameInfos;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = _client.Name;
            this._client.RetrieveSupportedGames();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this._client.RegisterNewParty((GameInfo) this.listBox1.SelectedItem);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _client.Dispose();
        }
    }
}