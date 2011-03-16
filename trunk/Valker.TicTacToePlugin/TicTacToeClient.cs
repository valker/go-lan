using System;
using System.Diagnostics;
using System.IO;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using System.Windows.Forms;

namespace Valker.TicTacToePlugin
{
    public class TicTacToeClient : IGameClient
    {
        public TicTacToeClient(Form parent, IMessageConnector connector, Func<string, IMessage> func)
        {
            Parent = parent;
            Connector = connector;
            CreateMessageDelegate = func;
        }

        protected Func<string, IMessage> CreateMessageDelegate { get; set; }

        protected IMessageConnector Connector { get; set; }

        protected Form Parent { get; set; }
        public string Name { get; set; }

        public Form CreatePlayingForm(string parameterString, string playerName)
        {
            var serializer = TicTacToeParameters.GetSerializer();
            var parameters = (TicTacToeParameters) serializer.Deserialize(new StringReader(parameterString));
            var form = new PlayingForm(parameters.Width, this) { Text = playerName };
            return form;
        }

        public string Parameters
        {
            get; set;
        }

        public event EventHandler AllowMove = delegate { };
        public event EventHandler Wait = delegate { };
        
        public void ExecuteMessage(string message)
        {
            switch (message)
            {
                case "AM":
                    AllowMove(this, EventArgs.Empty);
                    break;
                case "WA":
                    Wait(this, EventArgs.Empty);
                    break;
                default:
                    Debug.WriteLine("Unknown message: <" + message + ">");
                    break;
            }
        }

        public void ClickedOnBoard(MouseEventArgs args)
        {
            var text = string.Format("M<{0},{1}>", args.X, args.Y);
            Connector.Send(Name, "_server", CreateMessageDelegate(text).ToString());
        }
    }
}
