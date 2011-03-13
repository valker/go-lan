using System.IO;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Game;
using System.Windows.Forms;

namespace Valker.TicTacToePlugin
{
    class TicTacToeClient : IGameClient
    {
        public TicTacToeClient()
        {
            var form = new ParametersForm();
            if(form.ShowDialog() == DialogResult.OK)
            {
                Parameters = form.Parameters;
            }
        }

        public Form CreatePlayingForm(string parameterString)
        {
            var serializer = TicTacToeParameters.GetSerializer();
            var parameters = (TicTacToeParameters) serializer.Deserialize(new StringReader(parameterString));
            return new PlayingForm(parameters.Width);
        }

        public IGameParameters Parameters
        {
            get; set;
        }
    }
}
