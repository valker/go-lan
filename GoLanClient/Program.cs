
using GoLanClient.TextBased;

namespace GoLanClient
{
    using Engine;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            IEngine engine = new GoLanEngine();
            engine.Rules.Add(new Go.Rules());
            engine.Rules.Add(new OtherGame.Rules());

            //new Driver(engine).Run();
            new WinForms.Driver(engine).Run();
        }

    }
}
