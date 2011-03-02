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

        public override void Execute(IServerMessageExecuter server, object sender)
        {
            PartyStatus status = server.RegisterNewParty(this.Name, this.GameId, (IMessageConnector) sender);
            var message = new AcknowledgeRegistrationMessage(status == PartyStatus.PartyRegistred).ToString();
            ((IMessageConnector)sender).Send(message);
            server.UpdatePartyStates();
        }

        #endregion
    }
}
