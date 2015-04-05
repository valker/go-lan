using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin;

namespace Valker.PlayOnLan.GoPluginTests
{
    [TestFixture]
    public class EngineTest
    {
        [Test]
        public void TestSwitchingPlayer()
        {
            IPositionStorage positionStorage = new Mock<IPositionStorage>().Object;
            var playerProvider = Mock.Of<IPlayerProvider>(provider => provider.GetPlayers() == new IPlayer[]
            {
                Mock.Of<IPlayer>(player => player.PlayerName == "aaa"),
                Mock.Of<IPlayer>(player => player.PlayerName == "bbb"),
            });
            IRules rules = new Mock<IRules>().Object;
            IEngine engine = new Engine(positionStorage, playerProvider, rules);
            IPlayer current = engine.CurrentPlayer;
            Assert.That(current.PlayerName, Is.EqualTo("aaa").Or.EqualTo("bbb"));
            engine.Move(new Move(0,0));
            Assert.That(engine.CurrentPlayer, Is.EqualTo("aaa").Or.EqualTo("bbb"));
            Assert.That(engine.CurrentPlayer, Is.Not.EqualTo(current));
        }

        [Test]
        public void TestEatedSimple()
        {
            IPositionStorage positionStorage = new Mock<IPositionStorage>().Object;
            IPlayerProvider playerProvider = new Mock<IPlayerProvider>().Object;
            IRules rules = new Mock<IRules>().Object;
            IEngine engine = new Engine(positionStorage, playerProvider, rules);
            Assert.That(playerProvider.GetPlayers().Sum(p=>engine.GetScore(p)), Is.EqualTo(0));
            engine.Move(new Move(0,1));
            Assert.That(playerProvider.GetPlayers().Sum(p => engine.GetScore(p)), Is.EqualTo(0));
            engine.Move(new Move(0, 0));
            Assert.That(playerProvider.GetPlayers().Sum(p => engine.GetScore(p)), Is.EqualTo(0));
            engine.Move(new Move(1, 0));
            Assert.That(playerProvider.GetPlayers().Sum(p => engine.GetScore(p)), Is.EqualTo(1));
        }

        [Test]
        public void TestEatedChangedEvent()
        {
            var positionStorage = CreatePositionStorage();
            var playerProvider = Mock.Of<IPlayerProvider>(provider => provider.GetPlayers() == new IPlayer[]
            {
                Mock.Of<IPlayer>(player => player.PlayerName == "vvv"),
                Mock.Of<IPlayer>(player => player.PlayerName == "aaa"),
            });

            var rules =
                Mock.Of<IRules>(
                    rules1 =>
                        rules1.IsMoveAcceptableInPosition(It.IsAny<IMove>(), It.IsAny<IPosition>()) ==
                        Tuple.Create(true, ExceptionReason.None));

            IEngine engine = new Engine(positionStorage, playerProvider, rules);
            bool eventFired = false;
            engine.ScoreChanged += (sender, args) => eventFired = true;
            engine.Move(new Move(0, 1));
            Assert.That(eventFired, Is.EqualTo(false));
            engine.Move(new Move(0, 0));
            Assert.That(eventFired, Is.EqualTo(false));
            engine.Move(new Move(1, 0));
            Assert.That(eventFired, Is.EqualTo(true));
        }

        private static IPositionStorage CreatePositionStorage()
        {
            return Mock.Of<IPositionStorage>(storage => storage.Initial == CreatePosition());
        }

        private static IPosition CreatePosition()
        {
            var positionMock = new Mock<IPosition>();
            positionMock.Setup(position => position.Clone()).Returns(CreatePosition);
            positionMock.Setup(position => position.GetCellAt(It.IsAny<ICoordinates>())).Returns(new EmptyCell());
            return positionMock.Object;
        }

        [Test]
        public void TestFieldChangedEvent()
        {
            IPositionStorage positionStorage = new Mock<IPositionStorage>().Object;
            IPlayerProvider playerProvider = new Mock<IPlayerProvider>().Object;
            IRules rules = new Mock<IRules>().Object;
            IEngine engine = new Engine(positionStorage, playerProvider, rules);
            bool eventFired = false;
            engine.CellChanged += (sender, args) => eventFired = true;
            engine.Move(new Move(0, 0));
            Assert.That(eventFired, Is.EqualTo(true));
        }
    }
}