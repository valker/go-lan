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
    public class GroupTest
    {
        [Test]
        public void TestAddPoints()
        {
            var group = new Group(new Point(1,1), MokuState.Black );
            Assert.IsTrue(group.Count == 1);
            group.Add(new Point(1,2));
            Assert.IsTrue(group.Count == 2);
        }

        [ExpectedException(typeof(ArgumentException))]
        [Test]
        public void TestAddWithException()
        {
            var group = new Group(new Point(1, 1), MokuState.Black);
            group.Add(new Point(1, 1));
        }
    }
}
