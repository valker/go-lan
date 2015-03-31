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
        public ICellState CellState { get; private set; }

        public CellChangedEventArgs(ICoordinates coordinates, ICellState cellState)
        {
            Coordinates = coordinates;
            CellState = cellState;
        }

        public CellChangedEventArgs(string[] extractParams)
        {
            throw new NotImplementedException();
        }
    }
}