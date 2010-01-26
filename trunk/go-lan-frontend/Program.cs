using System;
using go_engine;

namespace go_lan_frontend
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                GameManager manager = new GameManager();
                game.Manager = manager;
                game.Run();
            }
        }
    }
}

