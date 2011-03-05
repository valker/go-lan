using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Game;
using System.Windows.Forms;

namespace Valker.TicTacToePlugin
{
    class TicTacToeClient : IGameClient
    {
        #region IGameClient Members

        public IGameParameters CreateParameters(Form parent)
        {
            var f = new ParametersForm();
            if (f.ShowDialog(parent) == DialogResult.OK)
            {
                return new TicTacToeParameters() { Width = int.Parse(f.txtWidth.Text), Stones = int.Parse(f.txtStones.Text) };
            }
            throw new NotImplementedException();
        }

        #endregion
    }
}
