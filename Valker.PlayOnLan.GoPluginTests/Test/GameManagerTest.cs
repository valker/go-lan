using System.Linq;
using NUnit.Framework;
using Valker.PlayOnLan.Api;
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

        [Test]
        public void TestSwitchingPlayer()
        {
            IEngine engine = new Engine(9);
            var current = engine.CurrentPlayer;
            Assert.That(current, Is.EqualTo(Stone.Black).Or.EqualTo(Stone.White));
            engine.Move(new Point());
            Assert.That(engine.CurrentPlayer, Is.EqualTo(Stone.Black).Or.EqualTo(Stone.White));
            Assert.That(engine.CurrentPlayer, Is.Not.EqualTo(current));
        }

        [Test]
        public void TestEatedSimple()
        {
            IEngine engine = new Engine(9);
            Assert.That(engine.Eated.Sum(pair => pair.Value), Is.EqualTo(0));
            engine.Move(new Point(0, 1));
            Assert.That(engine.Eated.Sum(pair => pair.Value), Is.EqualTo(0));
            engine.Move(new Point(0, 0));
            Assert.That(engine.Eated.Sum(pair => pair.Value), Is.EqualTo(0));
            engine.Move(new Point(1, 0));
            Assert.That(engine.Eated.Sum(pair => pair.Value), Is.EqualTo(1));
        }

        [Test]
        public void TestEatedChangedEvent()
        {
            IEngine engine = new Engine(9);
            bool eventFired = false;
            engine.EatedChanged += (sender, args) => eventFired = true;
            engine.Move(new Point(0, 1));
            Assert.That(eventFired, Is.EqualTo(false));
            engine.Move(new Point(0, 0));
            Assert.That(eventFired, Is.EqualTo(false));
            engine.Move(new Point(1, 0));
            Assert.That(eventFired, Is.EqualTo(true));
        }

        [Test]
        public void TestFieldChangedEvent()
        {
            IEngine engine = new Engine(9);
            bool eventFired = false;
            engine.FieldChanged += (sender, args) => eventFired = true;
            engine.Move(new Point(0, 0));
            Assert.That(eventFired, Is.EqualTo(true));
        }
    }
}