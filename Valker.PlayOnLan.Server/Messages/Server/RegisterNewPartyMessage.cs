using System;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public sealed class RegisterNewPartyMessage : ServerMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ServerMessage), new[] {typeof(RegisterNewPartyMessage)});

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

        public override void Execute(IServerMessageExecuter server, IAgentInfo agent)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (agent == null) throw new ArgumentNullException("agent");
            server.RegisterNewParty(agent, GameId, Parameters);
        }

        #endregion

        public override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
