using System;
using Autofac;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.GoPlugin.Abstract;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// Define the 'Go' ('baduk', 'weiqi') game for PlayOnLan framework
    /// </summary>
    public class GoGame : IGameType
    {
        private readonly ICoordinatesFactory _coordinatesFactory;

        public GoGame()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new CoordinatesFactory()).As<ICoordinatesFactory>();

            var container = builder.Build();
            _coordinatesFactory = container.Resolve<ICoordinatesFactory>();
        }

        /// <summary>
        /// Describes the name of the game on original language
        /// </summary>
        public string Name => "Go";

        /// <summary>
        /// Defines the unique identifier of the game type
        /// </summary>
        public string Id => "19873C0D-1E8B-40ce-9633-590A5601ECFD";

        /// <summary>
        /// Ask user about parameters of the game. It depends on the game type
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public string AskParam(IForm parent)
        {
            var p = new Parameters {Width = 9};
            return p.ToString();
        }

        /// <summary>
        /// Creates client component of the game
        /// </summary>
        /// <returns></returns>
        public IGameClient CreateClient(IForm parent, IPlayerProvider playerProvider)
        {
            return new GoClient(playerProvider, _coordinatesFactory);
        }

        /// <summary>
        /// Creates server component of the game
        /// </summary>
        /// <param name="players">array of player, which will participate</param>
        /// <param name="parameters">parameters of the game depended on the game type</param>
        /// <returns>the server componend of the game</returns>
        public IGameServer CreateServer(IPlayer[] players, string parameters)
        {
            return new GoServer(players, parameters, _coordinatesFactory);
        }
    }

    public class CoordinatesFactory : ICoordinatesFactory
    {
        public ICoordinates Create(int[] parts)
        {
            switch (parts.Length)
            {
                case 1:
                    return new OneDimensionCoordinates(parts[0]);
                case 2:
                    return new TwoDimensionsCoordinates(parts[0], parts[1]);
                default:
                    throw new ArgumentException("unsupported dimenstion of coordinates");
            }
        }
    }
}
