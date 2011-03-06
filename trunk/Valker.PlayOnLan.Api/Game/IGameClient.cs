using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Valker.PlayOnLan.Api.Game
{
    public interface IGameClient
    {
        IGameParameters CreateParameters(Form parent);
        Form CreatePlayingForm();
    }
}
