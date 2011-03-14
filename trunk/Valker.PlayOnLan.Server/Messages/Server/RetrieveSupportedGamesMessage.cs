using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Client;
using System;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public sealed class RetrieveSupportedGamesMessage : ServerMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ServerMessage), new[]{typeof(RetrieveSupportedGamesMessage)});

        #region Overrides of Message

        public override void Execute(IServerMessageExecuter server, IClientInfo sender)
        {
            if (server == null) throw new ArgumentNullException();
            string[] array = server.RetrieveSupportedGames();
            var message = new RetrieveSupportedGamesResponceMessage();
            message.Responce = array;
            server.Send(sender, message.ToString());
        }

        #endregion

        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}