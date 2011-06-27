using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IInfo
    {
        string Gui { get; }
        IPlayingForm CreatePlayingForm(string parameters, string playerName);
    }
}
