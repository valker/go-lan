using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TestXmlSerialization
{
    public interface ICommand
    {
        void Execute();
    }

    public abstract class CommandBase : ICommand {
        public abstract void Execute();
    }

    [Serializable]
    public class ChatCommand : CommandBase
    {
        public string Message { get; set; }
        public override void Execute()
        {
            Console.WriteLine(Message);
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            ChatCommand cmd = new ChatCommand() {Message = "Hello world!"};
            ICommand icommand = cmd;
            XmlSerializer serializer = new XmlSerializer(typeof(CommandBase), new Type[]{typeof(ChatCommand)});
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            serializer.Serialize(writer, icommand);
            var value = sb.ToString();
            Console.WriteLine(value);

            var sr = new StringReader(value);
            var cmd2 = (ICommand) serializer.Deserialize(sr);
            cmd2.Execute();
        }
    }
}
