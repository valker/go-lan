using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// ќписывает параметры событи€ изменени€ клетки пол€
    /// </summary>
    public class CellChangedEventArgs : EventArgs
    {
        /// <summary>
        /// ¬озвращает координаты клетки
        /// </summary>
        public ICoordinates Coordinates { get; private set; }
        /// <summary>
        /// ¬озвращает состо€ние клетки
        /// </summary>
        public ICell Cell { get; private set; }

        public CellChangedEventArgs(ICoordinates coordinates, ICell cell)
        {
            Contract.Requires(coordinates != null);
            Contract.Requires(cell != null);
            Coordinates = coordinates;
            Cell = cell;
        }

        public CellChangedEventArgs(string[] extractParams, IPlayerProvider playerProvider)
        {
            Contract.Requires(extractParams != null);
            Contract.Requires(extractParams.Length >= 2);
            Coordinates = CreateCoordinates(extractParams[0]);
            Cell = CreateCell(extractParams[1],  playerProvider);
        }

        private static ICell CreateCell(string s, IPlayerProvider playerProvider)
        {
            if(s.StartsWith("EMPTY"))
                return new EmptyCell();
            if (s.StartsWith("PLAYER"))
            {
                var name = s.Split(':')[1];
                return new PlayerCell(playerProvider.GetPlayers().First(player => player.PlayerName == name));
            }
            throw new InvalidOperationException(string.Format("Unknown command:{0}",s));
            throw new NotImplementedException();
        }

        private static ICoordinates CreateCoordinates(string s)
        {
            var items = s.Split(';').Select(s1 => int.Parse(s1, CultureInfo.InvariantCulture)).ToArray();
            switch (items.Count())
            {
                case 1:
                    return new OneDimension(items[0]);
                case 2:
                    return new TwoDimensionsCoordinates(items[0], items[1]);
                default:
                    throw new InvalidOperationException(string.Format("Unsupported coordinates dimension: {0}", items.Count()));
            }
        }
    }
}