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