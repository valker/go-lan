using System;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Client;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public sealed class RegisterNewPartyMessage : ServerMessage
    {
        private static XmlSerializer Serializer = new XmlSerializer(typeof(ServerMessage), new[] {typeof(RegisterNewPartyMessage)});

        public string GameId { get; set; }

        public string Parameters { get; set; }

        public RegisterNewPartyMessage()
        {
        }

        public RegisterNewPartyMessage(string gameId, string parameters)
        {
            GameId = gameId;
            Parameters = parameters;
        }

        #region Overrides of ServerMessage

        public override void Execute(IServerMessageExecuter server, IClientInfo client, object identifier)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (client == null) throw new ArgumentNullException("client");
            server.RegisterNewParty(client, GameId, Parameters);
        }

        #endregion

        public override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
