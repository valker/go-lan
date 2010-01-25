using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public interface IPosition
    {
        MokuField Field { get; }
        int Size { get; }
        bool IsEditable { get; }
        IPosition CopyMokuField();

        /// <summary>
        /// ��������� ��� �� �������� ��
        /// </summary>
        /// <param name="point">�����, � ������� �����</param>
        /// <param name="player">�����, ������� ������ ���</param>
        /// <returns>����� ������� � ����� ��������� ������</returns>
        Pair<IPosition, int> Move(Point point, MokuState player);
    }
}