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

        public override void Execute(IServerMessageExecuter server, IClientInfo client)
        {
            if (server == null) throw new ArgumentNullException();
            PartyStatus status = server.RegisterNewParty(client, GameId, Parameters);
            var message = new AcknowledgeRegistrationMessage(status == PartyStatus.PartyRegistred, Parameters).ToString();
            server.Send(client, message);
            server.UpdatePartyStates(null);
        }

        #endregion

        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
