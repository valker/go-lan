using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using System;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    /// <summary>
    /// Defines a message that contains information about games supported by server
    /// </summary>
    public sealed class RetrieveSupportedGamesResponceMessage : ClientMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ClientMessage), new[]{ typeof(RetrieveSupportedGamesResponceMessage)});

        #region Overrides of Message

        public override void Execute(IClientMessageExecuter client, IMessageConnector sender)
        {
            if (client == null) throw new ArgumentNullException();
            client.UpdateSupportedGames(sender, Responce);
        }

        #endregion

        public string[] Responce { get; set; }
        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}