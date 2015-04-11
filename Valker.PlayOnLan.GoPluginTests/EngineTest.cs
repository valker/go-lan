using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin;
using Valker.PlayOnLan.GoPlugin.Abstract;
using Valker.PlayOnLan.Server;

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
            var positionStorage = new Mock<IPositionStorage>();
            IPlayer white = Mock.Of<IPlayer>(player => player.PlayerName == "white");
            var startPosition = new Mock<IPosition>();
            startPosition.Setup(position => position.CompareScore(It.IsAny<IPosition>()))
                .Returns(() => new[] { new Tuple<IPlayer, double>(white, 1), });
            startPosition.Setup(position => position.CompareStoneField(It.IsAny<IPosition>())).Returns(() => new Tuple<ICoordinates, ICell>[0]);
            positionStorage.Setup(storage => storage.Initial).Returns(() => startPosition.Object);
            var playerProvider = new Mock<IPlayerProvider>();
            var rules = new Mock<IRules>();
            IEngine engine = new Engine(positionStorage.Object, playerProvider.Object, rules.Object);
            bool eventFired = false;
            engine.ScoreChanged += (sender, args) => eventFired = true;
            var move = new Mock<IMove>();
            move.Setup(move1 => move1.Perform(It.IsAny<IPosition>(), It.IsAny<IPlayerProvider>()))
                .Returns(Mock.Of<IMoveConsequences>);
            engine.Move(move.Object);
            Assert.That(eventFired, Is.EqualTo(true));
//
//
//            var playerProvider = Mock.Of<IPlayerProvider>(provider => provider.GetPlayers() == new IPlayer[]
//            {
//                Mock.Of<IPlayer>(player => player.PlayerName == "vvv"),
//                Mock.Of<IPlayer>(player => player.PlayerName == "aaa"),
//            });
//            var positionStorage = CreatePositionStorage(playerProvider);
//
//            var rules = Mock.Of<IRules>();
//
//            engine.Move(new Move(0, 1));
//            Assert.That(eventFired, Is.EqualTo(false));
//            engine.Move(new Move(0, 0));
//            Assert.That(eventFired, Is.EqualTo(false));
//            engine.Move(new Move(1, 0));
//            Assert.That(eventFired, Is.EqualTo(true));
        }

        private static IPositionStorage CreatePositionStorage(IPlayerProvider playerProvider)
        {
            return Mock.Of<IPositionStorage>(storage => storage.Initial == CreatePosition(playerProvider));
        }

        private static IPosition CreatePosition(IPlayerProvider playerProvider)
        {
            return new Mock<Position>(9, playerProvider).As<IPosition>().Object;
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