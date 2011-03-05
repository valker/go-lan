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
        private string gameId;

        public string GameId
        {
            get { return gameId; }
            set { gameId = value; }
        }
        private string parameters;

        public string Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        public RegisterNewPartyMessage()
        {
        }

        public RegisterNewPartyMessage(string gameId, string parameters)
        {
            this.gameId = gameId;
            this.parameters = parameters;
        }

        #region Overrides of ServerMessage

        public override void Execute(IServerMessageExecuter server, IClientInfo client)
        {
            PartyStatus status = server.RegisterNewParty(client, this.GameId, this.Parameters);
            var message = new AcknowledgeRegistrationMessage(status == PartyStatus.PartyRegistred).ToString();
            server.Send(client, message);
            server.UpdatePartyStates();
        }

        #endregion
    }
}
