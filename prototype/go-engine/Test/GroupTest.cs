using System;
using go_engine.Data;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace go_engine.Test
{
    [TestFixture]
    public class GroupTest
    {
        [Test]
        public void TestAddPoints()
        {
            var group = new Group(new Point(1,1), MokuState.Black );
            Assert.IsTrue(group.Count == 1);
            var group2 = group.AddPoint(new Point(1,2));
            Assert.IsTrue(group2.Count == 2);
            Assert.AreNotEqual(group,group2);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void TestAddWithException()
        {
            var group = new Group(new Point(1, 1), MokuState.Black);
            var group2 = group.AddPoint(new Point(1, 1));
        }
    }
}
