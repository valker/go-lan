using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Game;

namespace Valker.TicTacToePlugin
{
    class TicTacToeServer : IGameServer
    {
        private IGameType _gameType;

        public TicTacToeServer(TicTacToeGame ticTacToeGame)
        {
            // TODO: Complete member initialization
            this._gameType = ticTacToeGame;
        }
        public void RegisterNewParty(string playerName, IGameParameters parameters)
        {
            throw new NotImplementedException();
        }


        public string Name
        {
            get { return _gameType.Name; }
        }

        public string ID
        {
            get { return _gameType.ID; }
        }
    }
}
