using NUnit.Framework;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin;
using Valker.PlayOnLan.GoPlugin.Abstract;
using Valker.PlayOnLan.Server;

namespace Valker.PlayOnLan.GoPluginTests.Integration
{
    [TestFixture]
    public class EngineTest
    {
        private ICoordinatesFactory _coordinatesFactory;

        [SetUp]
        public void Setup()
        {
            _coordinatesFactory = new CoordinatesFactory();
        }

        [Test]
        public void EatCorner()
        {
            var aPlayer = new Player {PlayerName = "a"};
            var bPlayer = new Player {PlayerName = "b"};
            IPlayer[] players = {aPlayer, bPlayer, };
            IPlayerProvider playerProvider = new PlayerProvider(players);
            IPositionStorage positionStorage = new PositionStorage(9, playerProvider, _coordinatesFactory);
            IRules rules = new Rules() ;
            var engine = new Engine(positionStorage, playerProvider, rules);
            bool score = false;
            engine.ScoreChanged += (sender, args) => score = true;
            engine.Move(new Move(_coordinatesFactory.Create(new []{ 0, 0})));
            engine.Move(new Move(_coordinatesFactory.Create(new[] { 1, 0})));
            engine.Move(new Move(_coordinatesFactory.Create(new[] { 1, 1})));
            engine.Move(new Move(_coordinatesFactory.Create(new[] { 0, 1})));
            Assert.That(engine.CurrentPosition.GetCellAt(new TwoDimensionsCoordinates(0, 0)), Is.InstanceOf<EmptyCell>());
            Assert.That(engine.GetScore(aPlayer), Is.EqualTo(0.0));
            Assert.That(engine.GetScore(bPlayer), Is.EqualTo(1.0));
            Assert.That(score, Is.EqualTo(true));
        }
    }
}
