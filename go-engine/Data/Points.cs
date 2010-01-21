using System;

namespace go_engine.Data
{
    /// <summary>
    /// ����� ����� �������� ����� � ����� ������
    /// </summary>
    [Flags]
    public enum Points
    {
        None = 0,
        /// <summary>
        /// ������������ ������ ������
        /// </summary>
        Empty = 1,
        /// <summary>
        /// ������������ ����� �����
        /// </summary>
        Live = 2
    }
}