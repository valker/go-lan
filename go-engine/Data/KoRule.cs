namespace go_engine.Data
{
    /// <summary>
    /// Задаёт режим обработки правила Ко
    /// </summary>
    public enum KoRule
    {
        None = 0,
        /// <summary>
        /// Правило не проверяется
        /// </summary>
        No,
        /// <summary>
        /// Позиция не должна повторять позицию за ход до этого
        /// </summary>
        Simle,
        /// <summary>
        /// Позиция не должна повторяться никогда
        /// </summary>
        SuperPositioned,
        /// <summary>
        /// Позиция не должна повторяться никогда для каждого из игроков.
        /// Та же позиция для другого игрока допустима.
        /// </summary>
        SuperSituation
    }
}