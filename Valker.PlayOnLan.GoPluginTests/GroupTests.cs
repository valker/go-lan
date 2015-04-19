using Moq;
using NUnit.Framework;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPluginTests
{
    [TestFixture]
    public class GroupTests
    {
        private Group _group;
        private ICoordinates _point;

        [SetUp]
        public void Setup()
        {
            _point = Mock.Of<ICoordinates>();
            IPlayer player = Mock.Of<IPlayer>();
            _group = new Group(_point, player);
        }
    }
}
