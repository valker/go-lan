using System.IO;
using System.Text;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Game;

namespace Valker.TicTacToePlugin
{
    public class TicTacToeParameters : IGameParameters
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof (TicTacToeParameters));

        public TicTacToeParameters()
        {
        }

        public TicTacToeParameters(int stones, int width)
        {
            Stones = stones;
            Width = width;
        }

        public int Stones { get; set; }

        public int Width { get; set; }

        public override string ToString()
        {
            var serializer = GetSerializer();
            var stringBuilder = new StringBuilder();
            serializer.Serialize(new StringWriter(stringBuilder), this);
            return stringBuilder.ToString();
        }

        public static XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
