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
        public TicTacToeClient()
        {
            var form = new ParametersForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                Parameters = form.Parameters;
            }
        }

        public Form CreatePlayingForm()
        {
            return new PlayingForm();
        }

        public IGameParameters Parameters
        {
            get; set;
        }
    }
}
