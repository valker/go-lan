using System.IO;
using Valker.PlayOnLan.Api.Game;
using System.Windows.Forms;

namespace Valker.TicTacToePlugin
{
    class TicTacToeClient : IGameClient
    {
        public Form CreatePlayingForm(string parameterString)
        {
            var serializer = TicTacToeParameters.GetSerializer();
            var parameters = (TicTacToeParameters) serializer.Deserialize(new StringReader(parameterString));
            return new PlayingForm(parameters.Width);
        }

        public IGameParameters AskParams(Form parent)
        {
            var form = new ParametersForm();
            return form.ShowDialog(parent) == DialogResult.OK ? form.Parameters : null;
        }

        public IGameParameters Parameters
        {
            get; set;
        }
    }
}
