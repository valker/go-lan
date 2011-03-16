using System;
using Valker.PlayOnLan.Api;

namespace Valker.TicTacToePlugin
{
    public class FieldChangedEventArgs : EventArgs
    {
        public FieldChangedEventArgs(string[] strings)
        {
            X = Convert.ToInt32(strings[0]);
            Y = Convert.ToInt32(strings[1]);
            StoneState = (Stone) Enum.Parse(typeof (Stone), strings[2]);
        }

        public int Y { get; set; }

        public int X { get; set; }

        public Stone StoneState { get; set; }
    }
}