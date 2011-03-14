using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    public sealed class AcknowledgeRegistrationMessage : ClientMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ClientMessage), new[]{typeof(AcknowledgeRegistrationMessage)});

        public AcknowledgeRegistrationMessage()
        {
        }

        public AcknowledgeRegistrationMessage(bool status, string parameters)
        {
            Status = status;
            Parameters = parameters;
        }

        public string Parameters { get; set; }

        public bool Status { get; set; }

        #region Overrides of ClientMessage

        public override void Execute(IClientMessageExecuter client, object sender)
        {
            if (client == null) throw new ArgumentNullException();
            client.AcknowledgeRegistration(Status, Parameters);
        }

        #endregion

        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
