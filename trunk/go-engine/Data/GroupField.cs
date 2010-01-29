using Microsoft.Xna.Framework;

namespace go_engine.Data
{
    /// <summary>
    /// ����� ������� ������ �� ������
    /// </summary>
    internal class GroupField
    {
        private Group[,] _field;
        public GroupField(int size)
        {
            _field = new Group[size,size];
        }

        /// <summary>
        /// ����������� �����������
        /// </summary>
        /// <param name="parent">������������ ������</param>
        public GroupField(GroupField parent)
        {
            _field = (Group[,]) parent._field.Clone();
        }

        public Group GetAt(Point point)
        {
            return _field[point.Y, point.X];
        }

        public void SetAt(Point point, Group group)
        {
            _field[point.Y, point.X] = group;
        }
    }
}