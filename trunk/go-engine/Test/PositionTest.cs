using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var position = Position.CreateInitial(9);
            Assert.IsTrue(position.Size == 9);
        }
        [Test]
        public void TestEditable()
        {
            var position = Position.CreateInitial(9);
            Assert.IsTrue(position.IsEditable);
        }
        [Test]
        public void TestFirstMove()
        {
            var position = Position.CreateInitial(9);
            Rules rules = new Rules();
            rules.Ko = KoRule.No;
            rules.Points = Points.Empty;
            var status = position.Move(new Point(1, 1), MokuState.Black, rules);
            Assert.IsFalse(status.First.IsEditable);
            Assert.IsTrue(status.Second == 0);
            Assert.AreNotSame(status.First, position);
        }

    }
}
