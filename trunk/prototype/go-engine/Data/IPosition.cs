using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    public interface IPosition
    {
        MokuField Field { get; }
        bool IsEditable { get; }
        IPosition CopyMokuField();

        /// <summary>
        /// ��������� ��� �� �������� ��
        /// </summary>
        /// <param name="point">�����, � ������� �����</param>
        /// <param name="player">�����, ������� ������ ���</param>
        /// <returns>����� ������� � ����� ��������� ������</returns>
        Pair<IPosition, int> Move(Point point, MokuState player);

        IPosition GetParentPosition();

        System.Collections.Generic.IEnumerable<IPosition> GetChildrenPositions();

        void SetParent(IPosition parent);

        void AddChild(IPosition child);

        System.Collections.Generic.IDictionary<MokuState, int> GetTerritories();
    }
}