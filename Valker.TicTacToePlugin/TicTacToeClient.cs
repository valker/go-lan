using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Game;

namespace Valker.TicTacToePlugin
{
    class TicTacToeClient : IGameClient
    {
        #region IGameClient Members

        public IGameParameters CreateParameters()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
