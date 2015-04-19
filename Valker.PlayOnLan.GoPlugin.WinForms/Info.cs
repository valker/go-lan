using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin.WinForms
{
    public class Info : IInfo
    {
        public string Gui => "winforms";

        public IPlayingForm CreatePlayingForm(string parameters, string playerName, GoClient client)
        {
            return new PlayingForm(client);
        }
    }
}
