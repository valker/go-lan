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
    class PositionStorageTest
    {
        [Test]
        public void TestInitial()
        {
            const int size = 9;
            var storage = new PositionStorage(size);
            var position = storage.Initial;
            Assert.IsNotNull(position);
            Assert.IsTrue(position.Size == size);
            Assert.IsTrue(position.IsEditable);
        }

        [Test]
        public void TestEditInitial()
        {
            const int size = 9;
            var storage = new PositionStorage(size);
            var newPosition = storage.Edit(storage.Initial, new Point(1, 1), MokuState.Black);
            Assert.IsNotNull(newPosition);
            Assert.AreSame(newPosition, storage.Initial);
            Assert.IsTrue(newPosition.Size == size);
        }
    }
}
