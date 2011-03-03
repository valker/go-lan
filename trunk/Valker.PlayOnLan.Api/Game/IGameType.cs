﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: valker
 * Дата: 23.02.2011
 * Время: 8:41
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;

namespace Valker.PlayOnLan.Api.Game
{
    /// <summary>
    /// Description of GameType.
    /// </summary>
    public interface IGameType
    {
        /// <summary>
        /// Describes the name of the game on original language
        /// </summary>
        string Name {get;}

        /// <summary>
        /// Defines the unique identifier of the game type
        /// </summary>
        string ID {get;}

        /// <summary>
        /// Creates server component of the game
        /// </summary>
        /// <returns></returns>
        IGameServer CreateServer();

        /// <summary>
        /// Creates client component of the game
        /// </summary>
        /// <returns></returns>
        IGameClient CreateClient();
    }
}