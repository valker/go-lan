using Moq;
using NUnit.Framework;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPluginTests.Integration
{
    [TestFixture]
    public class KoTest
    {
        [Test]
        public void SimpleKoDetected()
        {
            var black = Mock.Of<IPlayer>(player => player.Order == 0 && player.PlayerName == "black");
            var white = Mock.Of<IPlayer>(player => player.Order == 1 && player.PlayerName == "white");
            var players = new[] { black, white };
            var playerProviderMock = new Mock<IPlayerProvider>();
            playerProviderMock.Setup(p => p.GetPlayers()).Returns(players);
            playerProviderMock.Setup(p => p.GetFirstPlayer()).Returns(white);
            playerProviderMock.Setup(p => p.GetNextPlayer(It.IsAny<IPlayer>())).Returns((IPlayer pp) => players[(pp.Order + 1) % players.Length]);
            var playerProvider = playerProviderMock.Object;
            ICoordinatesFactory coordinatesFactory = new CoordinatesFactory();
            IPosition position = new Position(9, playerProvider, coordinatesFactory);
            position.PutStone(new TwoDimensionsCoordinates(2, 1), black);
            position.PutStone(new TwoDimensionsCoordinates(2, 3), black);
            position.PutStone(new TwoDimensionsCoordinates(1, 2), black);
            position.PutStone(new TwoDimensionsCoordinates(3, 2), black);

            position.PutStone(new TwoDimensionsCoordinates(3, 1), white);
            position.PutStone(new TwoDimensionsCoordinates(4, 2), white);
            position.PutStone(new TwoDimensionsCoordinates(3, 3), white);

            var positionStorage = new PositionStorage(position);

            var rules = new Rules {Ko = KoRule.Simple, Score = ScoreRule.EmptyTerritory};
            var engine = new Engine(positionStorage, playerProvider, rules);

            engine.Move(new Move(new TwoDimensionsCoordinates(2,2)));

            GoException ex = Assert.Throws<GoException>(() => engine.Move(new Move(new TwoDimensionsCoordinates(3,2))));
            Assert.That(ex.Reason, Is.EqualTo(ExceptionReason.Ko));
        }
    }
}