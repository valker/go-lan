using System;
using System.Diagnostics;
using System.IO;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using System.Windows.Forms;
using Valker.PlayOnLan.Utilities;

namespace Valker.TicTacToePlugin
{
    public class TicTacToeClient : IGameClient
    {
        public TicTacToeClient(IForm parent)
        {
            Parent = parent;
        }

        protected IForm Parent { get; set; }

        /// <summary>
        /// Create the form for playing
        /// </summary>
        /// <param playerName="parameters">parameters of the party</param>
        /// <returns>Form to be shown</returns>
        public IPlayingForm CreatePlayingForm(string parameterString, string playerName, string gui)
        {
            var serializer = TicTacToeParameters.GetSerializer();
            var parameters = (TicTacToeParameters) serializer.Deserialize(new StringReader(parameterString));

            IPlayingForm form = null;
            if (gui == "winforms")
            {
                form = new PlayingForm(parameters.Width, this) { Text = playerName };
            }
            return form;
        }

        public event EventHandler<MessageEventArgs> OnMessageReady = delegate { };

        public event EventHandler AllowMove = delegate { };
        public event EventHandler Wait = delegate { };
        public event EventHandler<FieldChangedEventArgs> FieldChanged = delegate { };
        public event EventHandler<ShowMessageEventArgs> ShowMessage = delegate { };

        public void ExecuteMessage(string message)
        {
            switch (Util.ExtractCommand(message))
            {
                case "AM": // Allow move
                    AllowMove(this, EventArgs.Empty);
                    break;
                case "WA":  // Wait
                    Wait(this, EventArgs.Empty);
                    break;
                case "FC":  // Field Changed
                    FieldChanged(this, new FieldChangedEventArgs(Util.ExtractParams(message)));
                    break;
                case "MSG": // Message
                    ShowMessage(this, new ShowMessageEventArgs(Util.ExtractParams(message)));
                    break;
                default:
                    Debug.WriteLine("Unknown message: {" + message + "}");
                    break;
            }
        }

        public event EventHandler Closed;

        public void ClickedOnBoard(MouseEventArgs args)
        {
            var text = string.Format("M[{0},{1}]", args.X, args.Y);
            OnMessageReady(this, new MessageEventArgs("", "_server", text));
        }
    }
}
