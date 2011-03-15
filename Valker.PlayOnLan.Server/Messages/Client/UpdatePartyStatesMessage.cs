using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    /// <summary>
    /// Defines a message that contains information about parties states
    /// </summary>
    public sealed class UpdatePartyStatesMessage : ClientMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ClientMessage), new[]{typeof(UpdatePartyStatesMessage)});

        public UpdatePartyStatesMessage()
        {
        }

        public UpdatePartyStatesMessage(List<PartyState> requests)
        {
            if (requests == null) throw new ArgumentNullException();
            Info = requests.ToArray();
        }

        public PartyState[] Info { get; set; }

        #region Overrides of ClientMessage

        public override void Execute(IClientMessageExecuter client, IMessageConnector sender)
        {
            if (client == null) throw new ArgumentNullException();
            var connector = sender as IMessageConnector;
            if (connector == null) throw new ArgumentException();
            client.UpdatePartyStates(connector, Info);
        }

        #endregion

        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}