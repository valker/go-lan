using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Client;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public class RetrieveSupportedGamesMessage : ServerMessage
    {
        #region Overrides of Message

        public override void Execute(IServerMessageExecuter server, IClientInfo sender)
        {
            string[] array = server.RetrieveSupportedGames();
            var message = new RetrieveSupportedGamesResponceMessage();
            message.Responce = array;
            server.Send(sender, message.ToString());
        }

        #endregion
    }
}