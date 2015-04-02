using System;
using System.Collections.Generic;
using Valker.PlayOnLan.Api;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// Интерфейс описывает конкретную позицию в партии.
    /// Другими словами - состояние игры, когда должен ходить игрок
    /// </summary>
    public interface IPosition : ICloneable
    {
        ICellState GetStoneAt(ICoordinates coordinates);
        /// <summary>
        /// Игрок, который должен делать ход
        /// </summary>
        IPlayer CurrentPlayer { get; }

        void ChangeCellState(ICoordinates coordinates, ICellState currentPlayer);
        List<Group> GetNearestGroups(ICoordinates coordinates, IPlayer currentPlayer);
        void AddGroup(Group grp);
        void SetGroupAt(ICoordinates coordinates, Group grp);
        void RemoveGroup(Group grp);
        void ExcludeGroups(List<Group> groups);
    }
}
