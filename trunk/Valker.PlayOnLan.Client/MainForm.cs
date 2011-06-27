using System;
using System.Collections.Generic;
using System.Linq;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Client.Communication;
using Valker.PlayOnLan.Client2008.Communication;
using Valker.PlayOnLan.Server;

namespace Valker.PlayOnLan.Client2008
{
    /// <summary>
    /// This clas provides base functionality for any PlayOnLan clients
    /// </summary>
    public class MainForm
    {
        private ClientImpl _client;
        private Dictionary<string, string> _gameNames = new Dictionary<string, string>();
        private List<PartyInfo> _partyStates = new List<PartyInfo>();
        private IMainForm _mainForm;

        public MainForm(ClientImpl client, IMainForm mainForm)
        {
            _client = client;
            _mainForm = mainForm;
            _client.SupportedGamesChanged += ClientOnSupportedGamesChanged;
            _client.PartyStatesChanged += ClientOnPartyStatesChanged;
        }

        private void ClientOnPartyStatesChanged(object sender, PartyStatesArgs args)
        {
            UpdatePartyStatesData(args);
            InvokeOnUpdatePartyStatesView(EventArgs.Empty);
        }

        public event EventHandler OnUpdatePartyStatesView;

        private void InvokeOnUpdatePartyStatesView(EventArgs e)
        {
            EventHandler handler = OnUpdatePartyStatesView;
            if (handler != null) handler(this, e);
        }

        private void UpdatePartyStatesData(PartyStatesArgs args)
        {
            _partyStates.Clear();

            foreach (PartyState partyState in args.States)
            {
                _partyStates.Add(CreatePartyInfo(partyState, args.Connector));
            }
        }

        private PartyInfo CreatePartyInfo(PartyState state, IMessageConnector connector)
        {
            var value = new PartyInfo()
                            {
                                Connector = connector,
                                GameTypeId = state.GameTypeId,
                                GameName = _gameNames[state.GameTypeId],
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

                InvokeOnUpdateGameInfo(new UpdateGameInfoEventArgs(){GameInfos = gameInfos});
                //UpdateGameInfo(gameInfos);
            }
        }

        public event EventHandler<UpdateGameInfoEventArgs> OnUpdateGameInfo;

        private void InvokeOnUpdateGameInfo(UpdateGameInfoEventArgs e)
        {
            EventHandler<UpdateGameInfoEventArgs> handler = OnUpdateGameInfo;
            if (handler != null) handler(this, e);
        }


        //protected abstract void UpdateGameInfo(GameInfo[] infos);

        public void OnLoad()
        {
            InvokeOnSetPlayerName(new SetPlayerNameEventArgs(){Name = _client.Name});
            _client.RetrieveSupportedGames();
        }

        public event EventHandler<SetPlayerNameEventArgs> OnSetPlayerName;

        private void InvokeOnSetPlayerName(SetPlayerNameEventArgs e)
        {
            EventHandler<SetPlayerNameEventArgs> handler = OnSetPlayerName;
            if (handler != null) handler(this, e);
        }

        //protected void SetPlayerName(string name);

        public void Register()
        {
            var args = new GetSelectedGameInfoEventArgs();
            InvokeOnGetSelectedGameInfo(args);
            
            _client.RegisterNewParty(args.GameInfo, _mainForm);
        }

        //protected abstract GameInfo GetSelectedGameInfo();
        public event EventHandler<GetSelectedGameInfoEventArgs> OnGetSelectedGameInfo;

        private void InvokeOnGetSelectedGameInfo(GetSelectedGameInfoEventArgs e)
        {
            EventHandler<GetSelectedGameInfoEventArgs> handler = OnGetSelectedGameInfo;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// <remarks>should be called by derived class</remarks>
        /// </summary>
        protected void MainForm_FormClosed()
        {
            _client.Dispose();
        }

        public void Accept()
        {
            var args = new GetSelectedPartyInfoEventArgs();
            
            InvokeOnGetSelectedPartyInfo(args);
            PartyInfo partyInfo = args.PartyInfo;
            if (partyInfo == null) return;
            _client.AcceptParty(partyInfo);
        }

        public event EventHandler<GetSelectedPartyInfoEventArgs> OnGetSelectedPartyInfo;

        private void InvokeOnGetSelectedPartyInfo(GetSelectedPartyInfoEventArgs e)
        {
            EventHandler<GetSelectedPartyInfoEventArgs> handler = OnGetSelectedPartyInfo;
            if (handler != null) handler(this, e);
        }

        public IEnumerable<PartyInfo> GetPartyStates()
        {
            return _partyStates;
        }
    }

    public class GetSelectedGameInfoEventArgs : EventArgs
    {
        public GameInfo GameInfo { get; set; }
    }

    public class GetSelectedPartyInfoEventArgs : EventArgs
    {
        public PartyInfo PartyInfo { get; set; }
    }

    public class UpdateGameInfoEventArgs : EventArgs
    {
        public GameInfo[] GameInfos { get; set; }
    }

    public class SetPlayerNameEventArgs : EventArgs
    {
        public string Name { get; set; }
    }
}