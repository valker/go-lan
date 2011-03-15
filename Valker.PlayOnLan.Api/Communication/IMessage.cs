using System.Xml.Serialization;

namespace Valker.PlayOnLan.Api.Communication
{
    public interface IMessage
    {
        XmlSerializer GetSerializer();
    }
}