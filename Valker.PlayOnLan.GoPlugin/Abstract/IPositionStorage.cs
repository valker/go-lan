namespace Valker.PlayOnLan.GoPlugin.Abstract
{
    public interface IPositionStorage
    {
        /// <summary>
        /// ���������� �������� ������� � ������ ����
        /// </summary>
        IPosition Initial { get; }

        void AddChildPosition(IPosition parent, IPosition child);

        int ExistParent(IPosition knownChild, IPosition possibleParent);
    }
}