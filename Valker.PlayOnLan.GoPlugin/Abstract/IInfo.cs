using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin.Abstract
{
    public interface IInfo
    {
        string Gui { get; }
        IPlayingForm CreatePlayingForm(string parameters, string playerName, GoClient client);
    }
}
