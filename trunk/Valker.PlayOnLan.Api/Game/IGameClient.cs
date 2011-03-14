using System.Windows.Forms;

namespace Valker.PlayOnLan.Api.Game
{
    public interface IGameClient
    {
        Form CreatePlayingForm(string parameters);
        IGameParameters AskParams(Form parent);
        IGameParameters Parameters { get; }
    }
}