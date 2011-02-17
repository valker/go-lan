using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages
{
    public class RetrieveSupportedGamesMessage : ServerMessage
    {
        #region Overrides of Message

        public override void Execute(IServerMessageExecuter server)
        {
            var array = server.RetrieveSupportedGames();
            var message = new RetrieveSupportedGamesResponceMessage();
            message.Responce = array;
            var str = message.ToString();
            server.Send(str);
        }

        #endregion
    }
}
