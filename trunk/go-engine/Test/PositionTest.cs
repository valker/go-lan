using go_engine.Data;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace go_engine.Test
{
    [TestFixture]
    public class PositionTest
    {
        [Test]
        public void TestSize()
        {
            IPosition position = Position.CreateInitial(9);
            Assert.IsTrue(position.Field.Size == 9);
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
            var status = position.Move(new Point(1, 1), MokuState.Black);
            Assert.IsFalse(status.First.IsEditable);
            Assert.IsTrue(status.Second == 0);
            Assert.AreNotSame(status.First, position);
        }

    }
}
