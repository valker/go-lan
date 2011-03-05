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

        public IGameParameters CreateParameters()
        {
            var f = new ParametersForm();
            if (f.Show(parent) == DialogResult.OK)
            {
                return new TicTacToeParameters() { ColumnWidthChangedEventArgs = int.Parse(f.txtWidth) };
            }
            throw new NotImplementedException();
        }

        #endregion
    }
}
