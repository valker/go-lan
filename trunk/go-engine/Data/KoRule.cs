namespace go_engine.Data
{
    /// <summary>
    /// ����� ����� ��������� ������� ��
    /// </summary>
    public enum KoRule
    {
        None = 0,
        /// <summary>
        /// ������� �� �����������
        /// </summary>
        No,
        /// <summary>
        /// ������� �� ������ ��������� ������� �� ��� �� �����
        /// </summary>
        Simle,
        /// <summary>
        /// ������� �� ������ ����������� �������
        /// </summary>
        SuperPositioned,
        /// <summary>
        /// ������� �� ������ ����������� ������� ��� ������� �� �������.
        /// �� �� ������� ��� ������� ������ ���������.
        /// </summary>
        SuperSituation
    }
}