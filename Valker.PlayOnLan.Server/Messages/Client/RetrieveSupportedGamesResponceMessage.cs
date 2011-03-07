using Valker.PlayOnLan.Api.Communication;
using System;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public class RetrieveSupportedGamesResponceMessage : ClientMessage
    {
        #region Overrides of Message

        public override void Execute(IClientMessageExecuter client, object sender)
        {
            if (client == null) throw new ArgumentNullException();
            client.UpdateSupportedGames(sender, this.Responce);
        }

        #endregion

        public string[] Responce { get; set; }
    }
}