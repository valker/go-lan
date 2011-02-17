using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages
{
    public class RetrieveSupportedGamesMessage : Message
    {
        #region Overrides of Message

        public override void Execute(MessageExecuter executer)
        {
            var server = (ServerMessageExecuter) executer;
            var array = server.RetrieveSupportedGames();
            var message = new RetrieveSupportedGamesResponceMessage();
            message.Responce = array;
            var serializer = new XmlSerializer(typeof(Message), new [] {message.GetType()});
            var writer = new StringWriter();
            serializer.Serialize(writer, message);
            var str = writer.GetStringBuilder().ToString();
            server.Send(str);
        }

        #endregion
    }

    public class RetrieveSupportedGamesResponceMessage : Message
    {
        #region Overrides of Message

        public override void Execute(MessageExecuter executer)
        {
            var clientMessageExecuter = (ClientMessageExecuter) executer;
            clientMessageExecuter.UpdateSupportedGames(Responce);
        }

        #endregion

        public string[] Responce { get; set; }
    }
}
