using System.Xml.Serialization;
using NUnit.Framework;
using Valker.PlayOnLan.Server.Messages.Server;

namespace ServerTest
{
    [TestFixture]
    public class MessageSerializability
    {
        [Test]
        public void Simple()
        {
            var serializer = new XmlSerializer(typeof (ServerMessage), ServerMessageTypes.Types);
            serializer.ToString();
        }
    }
}
