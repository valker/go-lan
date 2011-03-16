using System;
using System.Diagnostics;
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

        public Form CreatePlayingForm(string parameterString, string playerName)
        {
            var serializer = TicTacToeParameters.GetSerializer();
            var parameters = (TicTacToeParameters) serializer.Deserialize(new StringReader(parameterString));
            var form = new PlayingForm(parameters.Width);
            form.Text = playerName;
            return form;
        }

        public string Parameters
        {
            get; set;
        }

        public void ExecuteMessage(string message)
        {
            switch (message)
            {
                case "AM":
                    break;
                default:
                    Debug.WriteLine("Unknown message: <" + message + ">");
                    break;
            }
            throw new NotImplementedException();
        }
    }
}
