using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages
{

    /// <summary>
    /// Base class for messages that are going between client and server
    /// </summary>
    public abstract class Message : IMessage
    {
        /// <summary>
        /// Transforms message to the string that can be passed by the transport
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return XmlSerializerImpl.Perform(GetSerializer(), this);   
        }

        public abstract XmlSerializer GetSerializer();
    }
}