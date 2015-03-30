using System;
using System.Collections.Generic;
using Valker.PlayOnLan.Api;

namespace Valker.PlayOnLan.GoPlugin
{
    public interface IEngine
    {
        event EventHandler EatedChanged;
        event EventHandler<FieldChangedEventArgs> FieldChanged;
        ICollection<KeyValuePair<Stone, int>> Eated { get; }
        Stone CurrentPlayer { get; }
        void Pass();

        /// <summary>
        /// Сделать ход текущим игроком
        /// </summary>
        /// <param name="point"></param>
        void Move(Utilities.Point point);
    }
}