using System.Windows.Forms;

namespace Valker.PlayOnLan.Api.Game
{
    /// <summary>
    /// Defines an interface of client part of game plugin
    /// </summary>
    public interface IGameClient
    {
        /// <summary>
        /// Create the form for playing
        /// </summary>
        /// <param playerName="parameters">parameters of the party</param>
        /// <returns>Form to be shown</returns>
        Form CreatePlayingForm(string parameters, string playerName);

        /// <summary>
        /// Gets or sets the parameters assosiated with the client
        /// </summary>
        string Parameters { get; set; }

        string Name { get; set; }

        void ExecuteMessage(string message);
    }
}