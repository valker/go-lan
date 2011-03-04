using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Server.Messages.Client;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public class RegisterNewPartyMessage : SingleServerMessage
    {
        public RegisterNewPartyMessage()
        {
        }

        public RegisterNewPartyMessage(GameInfo info) : base(info.Connector)
        {
            GameId = info.GameId;
        }

        public string GameId { get; set; }

        #region Overrides of ServerMessage

        public override void Execute(IServerMessageExecuter server, IClientInfo client)
        {
            PartyStatus status = server.RegisterNewParty(client, this.GameId);
            var message = new AcknowledgeRegistrationMessage(status == PartyStatus.PartyRegistred).ToString();
            server.Send(client, message);
            server.UpdatePartyStates();
        }

        #endregion
    }
}
