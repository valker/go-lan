using System;
using System.Collections.Generic;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IPositionStorage
    {
        /// <summary>
        /// ���������� �������� ������� � ������ ����
        /// </summary>
        IPosition Initial { get; }

        void AddChildPosition(IPosition parent, IPosition child);

        bool ExistParent(IPosition knownChild, IPosition possibleParent);
//        /// <summary>
//        /// ���������� �������, ������� ����������� ��� ������� ��������
//        /// ����� ������ �������
//        /// </summary>
//        /// <param name="position"></param>
//        /// <returns></returns>
//        IEnumerable<IPosition> GetChildPositions(IPosition position);
//        /// <summary>
//        /// ��������� ���
//        /// </summary>
//        /// <param name="position">�������� �������</param>
//        /// <param name="point">������� �����</param>
//        /// <param name="player">���� ����� ������</param>
//        /// <returns></returns>
//        Tuple<IPosition, IMoveInfo> Move(IPosition position, Point point, Stone player);
    }
}