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
            return XmlSerializerImpl.Perform(this.GetBaseClass(), this.GetType(), this);
        }

        protected abstract Type GetBaseClass();
    }

    public static class XmlSerializerImpl
    {
        public static string Perform(Type baseClass, Type thisType, object objectToSerialize)
        {
            var serializer = new XmlSerializer(baseClass, new[] {thisType});
            var writer = new StringWriter();
            serializer.Serialize(writer, objectToSerialize);
            return writer.GetStringBuilder().ToString();
        }
    }
}