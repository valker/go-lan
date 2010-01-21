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
    public class PositionStorageTest
    {
        private const int size = 9;

        private PositionStorage _storage;

        [TestFixtureSetUp]
        public void Setup()
        {
            _storage = new PositionStorage(size);
        }

        [Test]
        public void TestInitial()
        {
            var position = _storage.Initial;
            Assert.IsNotNull(position);
            Assert.IsTrue(position.Size == size);
            Assert.IsTrue(position.IsEditable);
        }

        [Test]
        public void TestEditInitial()
        {
            var newPosition = _storage.Edit(_storage.Initial, new Point(1, 1), MokuState.Black);
            Assert.IsNotNull(newPosition);
            Assert.AreSame(newPosition, _storage.Initial);
            Assert.IsTrue(newPosition.Size == size);
        }

        [Test]
        public void TestParentChildRelationship()
        {
            var count = _storage.GetChildPositions(_storage.Initial).Count();
            Assert.IsTrue(count == 0);
        }
    }
}
