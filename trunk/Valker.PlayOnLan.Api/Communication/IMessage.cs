using System.Xml.Serialization;

namespace Valker.PlayOnLan.Api.Communication
{
    /// <summary>
    /// Define serializable message interface
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Returns the xml serializer for given message
        /// </summary>
        /// <returns>xml serializer</returns>
        XmlSerializer GetSerializer();
    }
}