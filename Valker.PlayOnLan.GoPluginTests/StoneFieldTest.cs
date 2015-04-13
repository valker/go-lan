using System.Linq;
using NUnit.Framework;
using Valker.PlayOnLan.GoPlugin;
using Valker.PlayOnLan.Server;

namespace Valker.PlayOnLan.GoPluginTests
{
    [TestFixture]
    public class StoneFieldTest
    {
        [Test]
        public void TestEquals()
        {
            var a = new CellField(2);
            var b = new CellField(2);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void TestNotEquals()
        {
            var a = new CellField(2);
            var b = new CellField(3);
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void AccessNotInitializedFieldTest()
        {
            var field = new CellField(9);
            var cell = field.GetAt(new TwoDimensionsCoordinates(5, 5));
            Assert.That(cell, Is.TypeOf<EmptyCell>());
        }
        [Test]
        public void AccessFieldAsEnumerableTest()
        {
            var field = new CellField(9);
            field.SetAt(new TwoDimensionsCoordinates(5, 5), new PlayerCell(new Player()));
            Assert.That(field.GetEnumerator(), Is.Not.Null);
            Assert.That(field.Count(), Is.EqualTo(1));
        }
    }
}
