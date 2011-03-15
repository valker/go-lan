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
}