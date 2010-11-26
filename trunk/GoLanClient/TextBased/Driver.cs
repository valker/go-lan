using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoLanClient.Engine;

namespace GoLanClient.TextBased
{
    class Driver
    {
        private IEngine engine_;

        public Driver(IEngine engine)
        {
            engine_ = engine;
        }

        public void Run()
        {
            var rules = SelectRules(engine_.Rules);
            Console.WriteLine(rules);
        }
        private static IRules SelectRules(ICollection<IRules> collection)
        {
            while (true)
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    Console.WriteLine((i + 1) + " " + collection.ElementAt(i).ToString());
                }
                Console.Write("Select game:");
                var line = Console.ReadLine();
                if (line != null)
                {
                    int choose = int.Parse(line);
                    if (choose >= 1 && choose <= collection.Count)
                    {
                        return collection.ElementAt(choose - 1);
                    }
                }
            }
        }

    }
}