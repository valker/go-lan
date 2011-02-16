using System;
using Valker.PlayServer;

namespace Valker.PlayServerStarter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
//            IListener listener = new Listener();
//            listener.Start();
//            Console.WriteLine("Press any key...");
//            Console.ReadLine();
            IConnectionEstablisher establisher = new ConnectionEstablisher();
            establisher.ConnectionEstablished += ((sender, eventArgs) =>
                                                  Console.WriteLine(eventArgs.TcpClient.ToString()));
            establisher.Start(1000);

            System.Threading.Thread.Sleep(10000);

            establisher.Stop();
        }
    }
}