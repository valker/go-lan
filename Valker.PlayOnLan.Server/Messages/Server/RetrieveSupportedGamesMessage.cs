using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using System;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public sealed class RetrieveSupportedGamesMessage : ServerMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ServerMessage), new[]{typeof(RetrieveSupportedGamesMessage)});

        #region Overrides of Message

        public override void Execute(IServerMessageExecuter server, IClientInfo sender)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (sender == null) throw new ArgumentNullException("sender");
            server.RetrieveSupportedGames(sender);
        }

        #endregion

        public override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}