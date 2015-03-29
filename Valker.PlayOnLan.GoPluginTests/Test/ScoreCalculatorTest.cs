using NUnit.Framework;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.GoPlugin;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPluginTests.Test
{
    [TestFixture]
    public class ScoreCalculatorTest
    {
        [Test]
        public void TestEmptyField()
        {
            var p = Position.CreateInitial(3);
            var c = new ScoreCalculator();
            var r = c.Calculate(p);
            Assert.AreEqual(0, r[0].Second);
            Assert.AreEqual(0, r[1].Second);
        }

        [Test]
        public void TestOneStoneField()
        {
            var p = Position.CreateInitial(3);
            p = p.Move(new Point(0, 0), Stone.Black).First;
            var c = new ScoreCalculator();
            var r = c.Calculate(p);
            Assert.AreEqual(8, r[0].Second);
            Assert.AreEqual(0, r[1].Second);
        }

        [Test]
        public void TestTwoStonesField()
        {
            var p = Position.CreateInitial(3);
            p = p.Move(new Point(0, 0), Stone.Black).First;
            p = p.Move(new Point(2, 2), Stone.White).First;
            var c = new ScoreCalculator();
            var r = c.Calculate(p);
            Assert.AreEqual(0, r[0].Second);
            Assert.AreEqual(0, r[1].Second);
        }

        [Test]
        public void TestClosedTerritory()
        {
            var p = Position.CreateInitial(3);
            p = p.Move(new Point(1, 0), Stone.Black).First;
            p = p.Move(new Point(1, 1), Stone.Black).First;
            p = p.Move(new Point(0, 1), Stone.Black).First;
            p = p.Move(new Point(2, 2), Stone.White).First;
            var c = new ScoreCalculator();
            var r = c.Calculate(p);
            Assert.AreEqual(1, r[0].Second);
            Assert.AreEqual(0, r[1].Second);
        }
    }
}
