using Moq;
using NUnit.Framework;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin;

namespace Valker.PlayOnLan.GoPluginTests
{
    [TestFixture]
    public class PositionTest
    {
        [Test]
        public void AppendStoneToExistingGroup()
        {
            var  playerProviderMock = new Mock<IPlayerProvider>();
            IPlayerProvider playerProvider = playerProviderMock.Object;
            var position = new Position(9, playerProvider, new CoordinatesFactory());
            IPlayer player = Mock.Of<IPlayer>(player1 => player1.PlayerName == "a");
            position.PutStone(new TwoDimensionsCoordinates(0, 0), player);

            IGroup group = position.PutStone(new TwoDimensionsCoordinates(0, 1), player);

            Assert.That(group.Count, Is.EqualTo(2));
        }

        [Test]
        public void StoneConnectsTwoExistingGroups()
        {
            var playerProviderMock = new Mock<IPlayerProvider>();
            IPlayerProvider playerProvider = playerProviderMock.Object;
            var position = new Position(9, playerProvider, new CoordinatesFactory());
            IPlayer player = Mock.Of<IPlayer>(player1 => player1.PlayerName == "a");
            position.PutStone(new TwoDimensionsCoordinates(0, 0), player);
            position.PutStone(new TwoDimensionsCoordinates(1, 1), player);

            IGroup group = position.PutStone(new TwoDimensionsCoordinates(0, 1), player);

            Assert.That(group.Count, Is.EqualTo(3));
        }
    }
}