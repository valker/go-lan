using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;
using System;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public sealed class RetrieveSupportedGamesResponceMessage : ClientMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ClientMessage), new[]{ typeof(RetrieveSupportedGamesResponceMessage)});

        #region Overrides of Message

        public override void Execute(IClientMessageExecuter client, object sender)
        {
            if (client == null) throw new ArgumentNullException();
            client.UpdateSupportedGames(sender, this.Responce);
        }

        #endregion

        public string[] Responce { get; set; }
        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}