namespace go_engine.Data
{
    /// <summary>
    /// Задаёт причины возникновения исключения.
    /// </summary>
    public enum ExceptionReason
    {
        /// <summary>
        /// Позиция занята камнем
        /// </summary>
        Occupped,
        /// <summary>
        /// Нельзя оставлять свою группу мёртвой
        /// </summary>
        CannotPlaceDead,
        /// <summary>
        /// Ход запрещён по правилу КО
        /// </summary>
        Ko
    }
}