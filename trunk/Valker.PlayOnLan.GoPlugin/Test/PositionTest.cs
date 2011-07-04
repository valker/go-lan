using NUnit.Framework;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin.Test
{
    [TestFixture]
    public class PositionTest
    {
        [Test]
        public void TestSize()
        {
            IPosition position = Position.CreateInitial(9);
            Assert.IsTrue(position.Size == 9);
        }
        [Test]
        public void TestEditable()
        {
            IPosition position = Position.CreateInitial(9);
            Assert.IsTrue(position.IsEditable);
        }
        [Test]
        public void TestFirstMove()
        {
            IPosition position = Position.CreateInitial(9);
            var status = position.Move(new Point(1, 1), Stone.Black);
            Assert.IsFalse(status.First.IsEditable);
            Assert.IsTrue(status.Second == 0);
            Assert.AreNotSame(status.First, position);
        }

        [Test]
        public void TestEquals()
        {
            PositionStorage st1 = new PositionStorage(9);
            PositionStorage st2 = new PositionStorage(9);
            Assert.AreNotEqual(st1, st2);
            Assert.AreEqual(st1.Initial, st2.Initial);
        }
    }
}