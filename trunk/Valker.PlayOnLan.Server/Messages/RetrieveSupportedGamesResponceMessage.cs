using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages
{
    public class RetrieveSupportedGamesResponceMessage : ClientMessage
    {
        #region Overrides of Message

        public override void Execute(IClientMessageExecuter client, object sender)
        {
            client.UpdateSupportedGames(sender, this.Responce);
        }

        #endregion

        public string[] Responce { get; set; }
    }
}