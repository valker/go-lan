using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyGoban;
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
            List<string> data = new List<string>();
            foreach (KeyValuePair<GameIdentifier, PartyState> pair in this._partyStates)
            {
                data.Add(this.GetPartyInfoString(pair.Key, pair.Value));
            }
            listBox2.DataSource = data;
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
            return new GameIdentifier {Connector = connector, GameType =  state.GameId, Name = state.Name};
        }


        private void ClientOnMessageToShow(object sender, MessageEventArgs args)
        {
            MessageBox.Show(args.Message);
        }

        private void ClientOnSupportedGamesChanged(object sender, SupportedGamesChangedEventArgs args)
        {
            var gameInfos = args.Games.Select(s => new GameInfo(s, (IMessageConnector) args.Sender)).ToArray();
            foreach (GameInfo info in gameInfos)
            {
                _gameNames.Add(info.GameId, info.ToString());
            }
            this.listBox1.DataSource = gameInfos;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this._client.RetrieveSupportedGames();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this._client.RegisterNewParty(this.textBox1.Text, (GameInfo) this.listBox1.SelectedItem);
        }
    }
}