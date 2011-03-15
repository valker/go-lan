using System;
using System.IO;
using System.Xml.Serialization;

namespace Valker.PlayOnLan.Server.Messages
{
    public static class XmlSerializerImpl
    {
        public static string Perform(XmlSerializer serializer, object objectToSerialize)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (objectToSerialize == null) throw new ArgumentNullException("objectToSerialize");

            var writer = new StringWriter();
            serializer.Serialize(writer, objectToSerialize);
            return writer.GetStringBuilder().ToString();
        }
    }
}