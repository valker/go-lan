using System;

namespace Valker.PlayOnLan.Client.Communication
{
    public class SupportedGamesChangedEventArgs : EventArgs
    {
        public SupportedGamesChangedEventArgs(string[] games, object sender)
        {
            Games = games;
            Sender = sender;
        }

        public object Sender { get; set; }

        public string[] Games { get; set; }
    }
}