using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valker.Api
{
    /// <summary>
    /// Define an interface of playing field
    /// </summary>
    interface IPlayfield
    {
        void AddPlayer(IPlayer player);
        void AddViewer(IViewer viewer);
        void SendCommand(IPlayer sender, IGameCommand command);
    }
}
