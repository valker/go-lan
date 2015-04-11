using Moq;
using NUnit.Framework;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin;

namespace Valker.PlayOnLan.GoPluginTests
{
    [TestFixture]
    public class MoveTest
    {
        [Test]
        public void MoveOnNotEmptyCell()
        {
            var move = new Move(0, 0);
            IPosition position = Mock.Of<IPosition>();
            IPlayerProvider playerProvider = Mock.Of<IPlayerProvider>();
            Assert.Throws<GoException>(() => move.Perform(position, playerProvider));
        }
    }
}
