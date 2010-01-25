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

        [Test]
        public void TestInitial()
        {
            var storage = new PositionStorage(size);
            var position = storage.Initial;
            Assert.IsNotNull(position);
            Assert.IsTrue(position.Size == size);
            Assert.IsTrue(position.IsEditable);
        }

        [Test]
        public void TestEditInitial()
        {
            var storage = new PositionStorage(size);
            var newPosition = storage.Edit(storage.Initial, new Point(1, 1), MokuState.Black);
            Assert.IsNotNull(newPosition);
            Assert.AreSame(newPosition, storage.Initial);
            Assert.IsTrue(newPosition.Size == size);
        }

        [Test]
        public void TestMove()
        {
            var storage = new PositionStorage(size);
            var initial = storage.Initial;
            var firstMove = storage.Move(initial, new Point(1, 1), MokuState.Black);
            Assert.AreNotEqual(initial, firstMove.First);
            Assert.IsTrue(firstMove.Second == 0);
            var secondMove = storage.Move(firstMove.First, new Point(2, 2), MokuState.White);
            Assert.AreNotEqual(firstMove.First, secondMove.First);
            Assert.IsTrue(secondMove.Second == 0);
        }

        [Test]
        public void TestParentChildRelationship()
        {
            var storage = new PositionStorage(size);
            var count = storage.GetChildPositions(storage.Initial).Count();
            Assert.IsTrue(count == 0);
        }
    }
}
