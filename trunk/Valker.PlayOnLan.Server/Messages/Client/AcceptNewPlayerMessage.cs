﻿using System;
using System.Xml.Serialization;

namespace Valker.PlayOnLan.Server.Messages.Client
{
    /// <summary>
    /// Defines a message that is sent when new player has been accepted by the server
    /// </summary>
    public sealed class AcceptNewPlayerMessage : ClientMessage
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ClientMessage), new[]{ typeof(AcceptNewPlayerMessage)});

        public override void Execute(Api.Communication.IClientMessageExecuter client, object sender)
        {
            if (client == null) throw new ArgumentNullException();
            client.AcceptNewPlayer(Status);
        }

        public bool Status { get; set; }
        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
