using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Valker.PlayOnLan.Api;

namespace Valker.PlayOnLan.GoPlugin.Test
{
    [TestFixture]
    public class StoneFieldTest
    {
        [Test]
        public void TestEquals()
        {
            var a = new StoneField(2);
            var b = new StoneField(2);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void TestNotEquals()
        {
            var a = new StoneField(2);
            var b = new StoneField(3);
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }
    }
}
