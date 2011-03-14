using System;
using System.IO;
using System.Xml.Serialization;

namespace Valker.PlayOnLan.Server.Messages
{
    /// <summary>
    /// Base class for messages that are going between client and server
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// Transforms message to the string that can be passed by the transport
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return XmlSerializerImpl.Perform(GetSerializer(), this);   
        }

        protected abstract XmlSerializer GetSerializer();
    }

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