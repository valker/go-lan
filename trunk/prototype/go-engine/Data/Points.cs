using System;

namespace go_engine.Data
{
    /// <summary>
    /// Задаёт режим подсчёта очков в конце партии
    /// </summary>
    [Flags]
    public enum Points
    {
        None = 0,
        /// <summary>
        /// Подсчитываем пустые пункты
        /// </summary>
        Empty = 1,
        /// <summary>
        /// Подсчитываем живые камни
        /// </summary>
        Live = 2
    }
}