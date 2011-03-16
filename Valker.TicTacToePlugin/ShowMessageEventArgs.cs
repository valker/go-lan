using System;

namespace Valker.TicTacToePlugin
{
    public class ShowMessageEventArgs : EventArgs
    {
        public ShowMessageEventArgs(string[] strings)
        {
            Text = string.Join(",", strings);
        }

        public string Text { get; set; }
    }
}