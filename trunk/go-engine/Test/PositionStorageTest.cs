using System.Linq;
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
            Assert.IsTrue(position.Field.Size == size);
            Assert.IsTrue(position.IsEditable);
        }

        [Test]
        public void TestEditInitial()
        {
            var storage = new PositionStorage(size);
            var newPosition = storage.Edit(storage.Initial, new Point(1, 1), MokuState.Black);
            Assert.IsNotNull(newPosition);
            Assert.AreSame(newPosition, storage.Initial);
            Assert.IsTrue(newPosition.Field.Size == size);
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

        [Test]
        public void TestEatCorner()
        {
            var storage = new PositionStorage(size);
            var st1 = storage.Move(storage.Initial, new Point(0, 0), MokuState.Black);
            var st2 = storage.Move(st1.First, new Point(1, 0), MokuState.White);
            var st3 = storage.Move(st2.First, new Point(3, 3), MokuState.Black);
            var st4 = storage.Move(st3.First, new Point(0, 1), MokuState.White);
            Assert.IsTrue(st4.Second == 1);
            Assert.IsTrue(st4.First.Field.GetAt(new Point(0,0))==MokuState.Empty);
        }

        [Test]
        public void TestEatCombinedGroup()
        {
            var storage = new PositionStorage(size);
            var s1 = storage.Move(storage.Initial, new Point(1, 0), MokuState.Black);
            var s2 = storage.Move(s1.First, new Point(2, 0), MokuState.White);
            var s3 = storage.Move(s2.First, new Point(0, 1), MokuState.Black);
            var s4 = storage.Move(s3.First, new Point(0, 2), MokuState.White);
            var s5 = storage.Move(s4.First, new Point(0, 0), MokuState.Black);
            var s6 = storage.Move(s5.First, new Point(1, 1), MokuState.White);
            Assert.IsTrue(s6.Second == 3);
            Assert.IsTrue(s6.First.Field.GetAt(new Point(0,0)) == MokuState.Empty);
        }
    }
}
