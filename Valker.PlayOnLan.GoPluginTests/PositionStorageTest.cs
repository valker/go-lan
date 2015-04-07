using Moq;
using NUnit.Framework;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin;

namespace Valker.PlayOnLan.GoPluginTests
{
    [TestFixture]
    public class PositionStorageTest
    {
        private const int Size = 9;

        [Test]
        public void RepeatedPositionDetectionTest()
        {
            IPlayer playerA = Mock.Of<IPlayer>(player => player.PlayerName == "a");
            IPlayer playerB = Mock.Of<IPlayer>(player => player.PlayerName == "b");
            IPlayerProvider playerProvider =
                Mock.Of<IPlayerProvider>(provider => provider.GetPlayers() == new[] {playerA, playerB});
            var storage = new PositionStorage(3, playerProvider);

            var pos1 = storage.Initial;

            var pos2 = (IPosition)pos1.Clone();
            pos2.ChangeCellState(new TwoDimensionsCoordinates(0,0), new PlayerCell(playerA));
            storage.AddChildPosition(pos1, pos2);

            var pos3 = (IPosition)pos2.Clone();
            pos3.ChangeCellState(new TwoDimensionsCoordinates(0,0), new EmptyCell());
            storage.AddChildPosition(pos2, pos3);

            var pos4 = (IPosition)pos3.Clone();
            pos4.ChangeCellState(new TwoDimensionsCoordinates(0, 0), new PlayerCell(playerA));

            bool result = storage.ExistParent(pos3, pos4);
            Assert.That(result, Is.True);
        }

//        [Test]
//        public void TestInitial()
//        {
//            var storage = new PositionStorage(Size);
//            var position = storage.Initial;
//            Assert.IsNotNull(position);
//            Assert.IsTrue(position.Size == Size);
//            Assert.IsTrue(position.IsEditable);
//        }

//        [Test]
//        public void TestEditInitial()
//        {
//            var storage = new PositionStorage(Size);
//            var newPosition = storage.Edit(storage.Initial, new Point(1, 1), Stone.Black);
//            Assert.IsNotNull(newPosition);
//            Assert.AreSame(newPosition, storage.Initial);
//            Assert.IsTrue(newPosition.Size == Size);
//        }

//        [Test]
//        public void TestMove()
//        {
//            var storage = new PositionStorage(Size);
//            var initial = storage.Initial;
//            var firstMove = storage.Move(initial, new Point(1, 1), Stone.Black);
//            Assert.AreNotEqual(initial, firstMove.Item1);
//            Assert.IsTrue(firstMove.Item2.Eated == 0);
//            var secondMove = storage.Move(firstMove.Item1, new Point(2, 2), Stone.White);
//            Assert.AreNotEqual(firstMove.Item1, secondMove.Item1);
//            Assert.IsTrue(secondMove.Item2.Eated == 0);
//        }

//        [Test]
//        public void TestParentChildRelationship()
//        {
//            var storage = new PositionStorage(Size);
//            var count = storage.GetChildPositions(storage.Initial).Count();
//            Assert.IsTrue(count == 0);
//        }

//        [Test]
//        public void TestEatCorner()
//        {
//            var storage = new PositionStorage(Size);
//            var st1 = storage.Move(storage.Initial, new Point(0, 0), Stone.Black);
//            var st2 = storage.Move(st1.Item1, new Point(1, 0), Stone.White);
//            var st3 = storage.Move(st2.Item1, new Point(3, 3), Stone.Black);
//            var st4 = storage.Move(st3.Item1, new Point(0, 1), Stone.White);
//            Assert.IsTrue(st4.Item2.Eated == 1);
//            Assert.IsTrue(st4.Item1.GetCellAt(new Point(0,0))==Stone.None);
//        }

//        [Test]
//        public void TestEatCombinedGroup()
//        {
//            var storage = new PositionStorage(Size);
//            var s1 = storage.Move(storage.Initial, new Point(1, 0), Stone.Black);
//            var s2 = storage.Move(s1.Item1, new Point(2, 0), Stone.White);
//            var s3 = storage.Move(s2.Item1, new Point(0, 1), Stone.Black);
//            var s4 = storage.Move(s3.Item1, new Point(0, 2), Stone.White);
//            var s5 = storage.Move(s4.Item1, new Point(0, 0), Stone.Black);
//            var s6 = storage.Move(s5.Item1, new Point(1, 1), Stone.White);
//            Assert.IsTrue(s6.Item2.Eated == 3);
//            Assert.IsTrue(s6.Item1.GetCellAt(new Point(0,0)) == Stone.None);
//        }

//        [Test]
//        public void TestEatCombinedGroup2()
//        {
//            var pnts = new[]
//                           {
//                               new Point(1, 0), new Point(1, 1), new Point(2, 0), new Point(2, 1), new Point(0, 1),
//                               new Point(3, 0), new Point(3, 1), new Point(0, 0),
//                           };
//            var result = PerformMoves(pnts);
//            Assert.IsTrue(result.Item2.Eated == 2);
//            Assert.IsTrue(result.Item1.GetCellAt(new Point(1, 0)) == Stone.None);
//            Assert.IsTrue(result.Item1.GetCellAt(new Point(2, 0)) == Stone.None);
//        }

//        [Test]
//        public void TestWrongDead()
//        {
//            var pnts = new Point[]
//                           {
//                               new Point(1, 1), new Point(2, 1), 
//                               new Point(1, 0), new Point(2, 0), 
//                               new Point(1, 2), new Point(2, 2), 
//                               new Point(2, 3), new Point(1, 3), 
//                               new Point(2, 4), new Point(1, 4),
//                               new Point(2, 5), new Point(1, 5), 
//                               new Point(0, 2), new Point(0, 3),
//                           };
//            PerformMoves(pnts);
//        }

//        [Test]
//        [ExpectedException(typeof(GoException))]
//        public void TestKo()
//        {
//            PerformMoves(new Point[]
//                             {
//                                 new Point(2,1),new Point(3,1),
//                                 new Point(3,2),new Point(2,2),
//                                 new Point(2,3),new Point(3,3),
//                                 new Point(1,7),new Point(4,2),
//                                 new Point(1,2),new Point(4,7),
//                                 new Point(3,2),new Point(2,2),                       
//                             });
//        }

//        public static Tuple<IPosition, IMoveInfo> PerformMoves(IEnumerable<Point> pnts)
//        {
//            var storage = new PositionStorage(Size);
//            var player = Stone.Black;
//            var pos = storage.Initial;
//            int i = 0;
//            var result = new Tuple<IPosition, IMoveInfo>(null, null);
//            foreach (var pnt in pnts)
//            {
//                result = storage.Move(pos, pnt, player);
//                player = Util.Opposite(player);
//                pos = result.Item1;
//                ++i;
//            }
//            return result;
//        }
    }
}
