using NUnit.Framework;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin.Test
{
    [TestFixture]
    public class GameManagerTest
    {
        [Test]
        public void TestWithoutKo()
        {
            var mgr = new Engine(9);
            mgr.Move(new Point(0,0));
        }
    }
}