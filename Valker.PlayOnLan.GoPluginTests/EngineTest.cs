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
            var playerAaa = Mock.Of<IPlayer>(player => player.PlayerName == "aaa");
            var playerBbb = Mock.Of<IPlayer>(player => player.PlayerName == "bbb");
            var startPosition = new Mock<IPosition>();
            startPosition.Setup(position => position.CurrentPlayer).Returns(() => playerAaa);
            var positionStorageMock = new Mock<IPositionStorage>();
            positionStorageMock.Setup(storage => storage.Initial).Returns(startPosition.Object);
            IPositionStorage positionStorage = positionStorageMock.Object;
            var playerProvider = Mock.Of<IPlayerProvider>(provider => provider.GetPlayers() == new IPlayer[]
            {
                playerAaa,
                playerBbb,
            } && provider.GetFirstPlayer() == playerAaa);
            IRules rules = new Mock<IRules>().Object;

            IEngine engine = new Engine(positionStorage, playerProvider, rules);

            Assert.That(engine.CurrentPlayer, Is.EqualTo(playerAaa));

            var moveMock = new Mock<IMove>();
            var moveConsequencesMock = new Mock<IMoveConsequences>();
            var newPositionMock = new Mock<IPosition>();
            newPositionMock.Setup(position => position.CurrentPlayer).Returns(playerBbb);
            moveConsequencesMock.Setup(consequences => consequences.Position).Returns(newPositionMock.Object);
            moveMock.Setup(move => move.Perform(It.IsAny<IPosition>(), It.IsAny<IPlayerProvider>())).Returns(
                () => moveConsequencesMock.Object);

            engine.Move(moveMock.Object);

            Assert.That(engine.CurrentPlayer, Is.EqualTo(playerBbb));
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
            var move = new Mock<IMove>();
            move.Setup(move1 => move1.Perform(It.IsAny<IPosition>(), It.IsAny<IPlayerProvider>()))
                .Returns(Mock.Of<IMoveConsequences>);
            bool eventFired = false;

            IEngine engine = new Engine(positionStorage.Object, playerProvider.Object, rules.Object);
            engine.ScoreChanged += (sender, args) => eventFired = true;
            engine.Move(move.Object);

            Assert.That(eventFired, Is.EqualTo(true));
        }

        [Test]
        public void TestFieldChangedEvent()
        {
            var positionStorage = new Mock<IPositionStorage>();
            var startPosition = new Mock<IPosition>();
            startPosition.Setup(position => position.CompareScore(It.IsAny<IPosition>()))
                .Returns(() => new Tuple<IPlayer, double>[0]);
            startPosition.Setup(position => position.CompareStoneField(It.IsAny<IPosition>())).Returns(() => new Tuple<ICoordinates, ICell>[]
            {
                Tuple.Create(Mock.Of<ICoordinates>(), Mock.Of<ICell>()), 
            });
            positionStorage.Setup(storage => storage.Initial).Returns(() => startPosition.Object);
            var playerProvider = new Mock<IPlayerProvider>();
            var rules = new Mock<IRules>();
            var move = new Mock<IMove>();
            move.Setup(move1 => move1.Perform(It.IsAny<IPosition>(), It.IsAny<IPlayerProvider>()))
                .Returns(Mock.Of<IMoveConsequences>);
            bool eventFired = false;

            IEngine engine = new Engine(positionStorage.Object, playerProvider.Object, rules.Object);
            engine.CellChanged += (sender, args) => eventFired = true;
            engine.Move(move.Object);

            Assert.That(eventFired, Is.EqualTo(true));
        }
    }
}