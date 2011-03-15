using System;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public sealed class AcknowledgeDropMessage : ClientMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ClientMessage), new []{typeof(AcknowledgeDropMessage)});

        #region Overrides of ClientMessage

        public override void Execute(IClientMessageExecuter client, IMessageConnector sender)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
