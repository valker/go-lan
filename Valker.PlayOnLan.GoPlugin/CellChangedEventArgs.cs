using System;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// ��������� ��������� ������� ��������� ������ ����
    /// </summary>
    public class CellChangedEventArgs : EventArgs
    {
        /// <summary>
        /// ���������� ���������� ������
        /// </summary>
        public ICoordinates Coordinates { get; private set; }
        /// <summary>
        /// ���������� ��������� ������
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