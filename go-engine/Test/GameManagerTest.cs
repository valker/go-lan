using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace go_engine.Test
{
    [TestFixture]
    public class GameManagerTest
    {
        [Test]
        public void TestWithoutKo()
        {
            GameManager mgr = new GameManager();
            mgr.Move(new Point(0,0));
        }
    }
}
