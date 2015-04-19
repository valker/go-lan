using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin.Abstract
{
    [ContractClass(typeof(EngineContract))]
    public interface IEngine : INotifyPropertyChanged
    {
        /// <summary>
        /// Сообщение об изменении состояния ячейки поля
        /// </summary>
        event EventHandler<CellChangedEventArgs> CellChanged;
        /// <summary>
        /// Счёт изменился
        /// </summary>
        event EventHandler<ScoreChangedEventArgs> ScoreChanged;
        /// <summary>
        /// Получить очки игрока
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        double GetScore(IPlayer player);
        /// <summary>
        /// Текущий игрок
        /// </summary>
        IPlayer CurrentPlayer { get; }

        IPosition CurrentPosition { get; }
        IPlayerProvider PlayerProvider { get; }

        /// <summary>
        /// Игрок делает ход, в результате либо меняется состояние (cells, score, current player)
        /// либо выдаётся исключение, если ход недопустимый
        /// </summary>
        /// <param name="move"></param>
        bool Move(IMove move);

        // Вопросы:
        // нужно ли включать в этот интерфейс описатель правил
    }

    [ContractClassFor(typeof(IEngine))]
    public abstract class EngineContract : IEngine
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<CellChangedEventArgs> CellChanged;
        public event EventHandler<ScoreChangedEventArgs> ScoreChanged;
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

        public bool Move(IMove move)
        {
            Contract.Requires(move != null);
            throw new NotImplementedException();
        }
    }
}