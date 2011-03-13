using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Client;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public class RegisterNewPartyMessage : SingleServerMessage
    {
        public string GameId { get; set; }

        public string Parameters { get; set; }

        public RegisterNewPartyMessage()
        {
        }

        public RegisterNewPartyMessage(string gameId, string parameters)
        {
            this.GameId = gameId;
            this.Parameters = parameters;
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
    }
}
