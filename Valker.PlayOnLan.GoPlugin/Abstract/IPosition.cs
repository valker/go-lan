using System;
using System.Collections.Generic;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// Интерфейс описывает конкретную позицию в партии.
    /// Другими словами - состояние игры, когда должен ходить игрок
    /// </summary>
    public interface IPosition : ICloneable
    {
        /// <summary>
        /// Получить состояние клетки по координатам
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        ICell GetCellAt(ICoordinates coordinates);
        /// <summary>
        /// Игрок, который должен делать ход в этой позиции
        /// </summary>
        IPlayer CurrentPlayer { get; }
        /// <summary>
        /// Изменить состояние клетки
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="currentPlayer"></param>
        void ChangeCellState(ICoordinates coordinates, ICell currentPlayer);

        Group PutStone(ICoordinates coordinates, IPlayer player);
        /// <summary>
        /// Проверить, существует ли клетка с такими координатами в этой позиции
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        bool Exist(ICoordinates coordinates);
        /// <summary>
        /// Сравнить положение камней в этой позиции и заданной
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        IEnumerable<Tuple<ICoordinates, ICell>> CompareStoneField(IPosition position);
        /// <summary>
        /// Сравнить счёт игроков этой позиции и заданной
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        IEnumerable<Tuple<IPlayer, double>> CompareScore(IPosition position);
        /// <summary>
        /// Получить счёт определённого игрока в данной позиции
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        double GetScore(IPlayer player);
        /// <summary>
        /// Выполнить ход по базовым правилам го: постановка камня, удаление мёртвых групп, подсчёт количества съеденных камней
        /// </summary>
        /// <param name="playerProvider"></param>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        IMoveConsequences MoveConsequences(IPlayerProvider playerProvider, ICoordinates coordinates);
    }
}
