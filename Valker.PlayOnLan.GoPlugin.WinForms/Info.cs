using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin.WinForms
{
    public class Info : IInfo
    {
        public string Gui
        {
            get { return "winforms"; }
        }

        public IPlayingForm CreatePlayingForm(string parameters, string playerName)
        {
            return new PlayingForm();
        }
    }
}
