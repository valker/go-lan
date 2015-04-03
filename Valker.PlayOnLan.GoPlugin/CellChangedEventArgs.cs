using System;

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
            Coordinates = coordinates;
            Cell = cell;
        }

        public CellChangedEventArgs(string[] extractParams)
        {
            throw new NotImplementedException();
        }
    }
}