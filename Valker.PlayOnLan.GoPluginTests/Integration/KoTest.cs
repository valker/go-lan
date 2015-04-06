using Moq;
using NUnit.Framework;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin;

namespace Valker.PlayOnLan.GoPluginTests.Integration
{
    [TestFixture]
    public class KoTest
    {
        [Test]
        [ExpectedException(typeof (GoException))]
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
            IPosition position = new Position(9, playerProvider);
            Move.PutStone(position, new TwoDimensionsCoordinates(2, 1), black);
            Move.PutStone(position, new TwoDimensionsCoordinates(2, 3), black);
            Move.PutStone(position, new TwoDimensionsCoordinates(1, 2), black);
            Move.PutStone(position, new TwoDimensionsCoordinates(3, 2), black);

            Move.PutStone(position, new TwoDimensionsCoordinates(3, 1), white);
            Move.PutStone(position, new TwoDimensionsCoordinates(4, 2), white);
            Move.PutStone(position, new TwoDimensionsCoordinates(3, 3), white);

            var positionStorage = new PositionStorage(position);

            var rules = new Rules {Ko = KoRule.Simple, Score = ScoreRule.EmptyTerritory};
            var engine = new Engine(positionStorage, playerProvider, rules);

            engine.Move(new Move(2, 2));
            engine.Move(new Move(3, 2));
        }
    }
}