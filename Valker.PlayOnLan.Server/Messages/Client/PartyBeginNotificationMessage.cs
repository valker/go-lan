using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Server2008;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public sealed class PartyBeginNotificationMessage : ClientMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof (ClientMessage), new[]
        {
            typeof (PartyBeginNotificationMessage),
        });

        public PartyBeginNotificationMessage()
        {
        }

        public PartyBeginNotificationMessage(int partyId, string gameTypeId, string parameters, string[] players, string thisPlayer)
        {
            GameTypeId = gameTypeId;
            PartyId = partyId;
            Parameters = parameters;
            Players = players;
            ThisPlayer = thisPlayer;
        }

        public string GameTypeId { get; set; }
        public int PartyId { get; set; }

        public override XmlSerializer GetSerializer()
        {
            return Serializer;
        }

        #region Overrides of ClientMessage

        public override void Execute(IClientMessageExecuter client, IMessageConnector sender)
        {
            client.PartyBeginNotification(sender, PartyId, GameTypeId, Parameters, Players, ThisPlayer);
        }

        public string Parameters { get; set; }

        public string[] Players { get; set; }
        public string ThisPlayer { get; set; }

        #endregion
    }
}