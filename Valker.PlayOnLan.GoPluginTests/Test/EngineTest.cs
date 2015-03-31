using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin.Test
{
    [TestFixture]
    public class EngineTest
    {
        [Test]
        public void TestSwitchingPlayer()
        {
            IPositionStorage positionStorage = new Mock<IPositionStorage>().Object;
            IPlayerProvider playerProvider = new Mock<IPlayerProvider>().Object;
            IRules rules = new Mock<IRules>().Object;
            IEngine engine = new Engine(positionStorage, playerProvider, rules);
            var current = engine.CurrentPlayer;
            Assert.That(current, Is.EqualTo(Stone.Black).Or.EqualTo(Stone.White));
            engine.Move(new Move(0,0));
            Assert.That(engine.CurrentPlayer, Is.EqualTo(Stone.Black).Or.EqualTo(Stone.White));
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
            var positionStorageMock = new Mock<IPositionStorage>();
            positionStorageMock.Setup(storage => storage.Initial).Returns((IPosition)null);
            var playerProviderMock = new Mock<IPlayerProvider>();
            var player1Mock = new Mock<IPlayer>(); player1Mock.SetupGet(player => player.PlayerName).Returns("a");
            var player2Mock = new Mock<IPlayer>(); player2Mock.SetupGet(player => player.PlayerName).Returns("b");
            playerProviderMock.Setup(provider => provider.GetPlayers())
                .Returns(new IPlayer[] {player1Mock.Object, player2Mock.Object });

            var rulesMock = new Mock<IRules>();
            rulesMock
                .Setup(rules => rules.IsMoveAcceptableInPosition(It.IsAny<IMove>(), It.IsAny<IPosition>()))
                .Returns((IMove move, IPosition position) => Tuple.Create(true, ExceptionReason.None));
            IEngine engine = new Engine(positionStorageMock.Object, playerProviderMock.Object, rulesMock.Object);
            bool eventFired = false;
            engine.ScoreChanged += (sender, args) => eventFired = true;
            engine.Move(new Move(0, 1));
            Assert.That(eventFired, Is.EqualTo(false));
            engine.Move(new Move(0, 0));
            Assert.That(eventFired, Is.EqualTo(false));
            engine.Move(new Move(1, 0));
            Assert.That(eventFired, Is.EqualTo(true));
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