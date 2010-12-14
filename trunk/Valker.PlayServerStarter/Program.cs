using System;
using Valker.PlayServer;

namespace Valker.PlayServerStarter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IListener listener = new Listener();
            listener.Start();
            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }
    }
}