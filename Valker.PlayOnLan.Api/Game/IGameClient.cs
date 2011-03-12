using System.Windows.Forms;

namespace Valker.PlayOnLan.Api.Game
{
    public interface IGameClient
    {
        Form CreatePlayingForm();
        IGameParameters Parameters { get; set; }
    }
}