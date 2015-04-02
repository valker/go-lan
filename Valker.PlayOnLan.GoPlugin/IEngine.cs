using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    [ContractClass(typeof(EngineContract))]
    public interface IEngine : INotifyPropertyChanged
    {
        /// <summary>
        /// ��������� �� ��������� ��������� ������ ����
        /// </summary>
        event EventHandler<CellChangedEventArgs> CellChanged;
        /// <summary>
        /// ���� ���������
        /// </summary>
        event EventHandler ScoreChanged;
        /// <summary>
        /// �������� ���� ������
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        double GetScore(IPlayer player);
        /// <summary>
        /// ������� �����
        /// </summary>
        IPlayer CurrentPlayer { get; }

        IPosition CurrentPosition { get; }
        IPlayerProvider PlayerProvider { get; }

        /// <summary>
        /// ����� ������ ���, � ���������� ���� �������� ��������� (cells, score, current player)
        /// ���� ������� ����������, ���� ��� ������������
        /// </summary>
        /// <param name="move"></param>
        void Move(IMove move);

        // �������:
        // ����� �� �������� � ���� ��������� ��������� ������
    }

    [ContractClassFor(typeof(IEngine))]
    public abstract class EngineContract : IEngine
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<CellChangedEventArgs> CellChanged;
        public event EventHandler ScoreChanged;
        public double GetScore(IPlayer player)
        {
            Contract.Requires(player != null);
            return default(double);
        }

        public IPlayer CurrentPlayer
        {
            get
            {
                Contract.Ensures(Contract.Result<IPlayer>() != null);
                return default(IPlayer);
            }
        }

        public IPosition CurrentPosition
        {
            get
            {
                Contract.Ensures(Contract.Result<IPosition>() != null);
                return default(IPosition);
            }
        }

        public IPlayerProvider PlayerProvider
        {
            get {
                Contract.Ensures(Contract.Result<IPlayerProvider>() != null);
                return default(IPlayerProvider);
            }
        }

        public void Move(IMove move)
        {
            Contract.Requires(move != null);
            throw new NotImplementedException();
        }
    }
}