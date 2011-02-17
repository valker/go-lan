using System;
using System.IO;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages
{


    public abstract class Message
    {
        public override string ToString()
        {
            var serializer = new XmlSerializer(typeof (Message), new []{typeof (RetrieveSupportedGamesMessage)});
            var writer = new StringWriter();
            serializer.Serialize(writer, this);
            return writer.GetStringBuilder().ToString();
        }

        public abstract void Execute(MessageExecuter executer);
    }
}