using System.IO;
using Valker.PlayOnLan.Api.Game;
using System.Windows.Forms;

namespace Valker.TicTacToePlugin
{
    class TicTacToeClient : IGameClient
    {
        public TicTacToeClient(Form parent)
        {
            Parent = parent;
        }

        protected Form Parent { get; set; }

        public Form CreatePlayingForm(string parameterString)
        {
            var serializer = TicTacToeParameters.GetSerializer();
            var parameters = (TicTacToeParameters) serializer.Deserialize(new StringReader(parameterString));
            return new PlayingForm(parameters.Width);
        }

        public string Parameters
        {
            get; set;
        }
    }
}
