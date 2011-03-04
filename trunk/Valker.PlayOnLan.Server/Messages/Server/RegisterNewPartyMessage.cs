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

        public RegisterNewPartyMessage(string name, GameInfo info) : base(info.Connector)
        {
            Name = name;
            GameId = info.GameId;
        }

        public string GameId { get; set; }

        public string Name { get; set; }

        #region Overrides of ServerMessage

        public override void Execute(IServerMessageExecuter server, IClientInfo client)
        {
            PartyStatus status = server.RegisterNewParty(client, this.Name, this.GameId);
            var message = new AcknowledgeRegistrationMessage(status == PartyStatus.PartyRegistred).ToString();
            client.ClientConnector.Send(client.ClientIdentifier, message);
            server.UpdatePartyStates();
        }

        #endregion
    }
}
