/*
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
        string Id {get;}

        /// <summary>
        /// Ask user about parameters of the game. It depends on the game type
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        string AskParam(IForm parent);

        /// <summary>
        /// Creates client component of the game
        /// </summary>
        /// <returns></returns>
        IGameClient CreateClient(IForm parent);

        /// <summary>
        /// Creates server component of the game
        /// </summary>
        /// <param name="players">array of player, which will participate</param>
        /// <param name="parameters">parameters of the game depended on the game type</param>
        /// <returns>the server componend of the game</returns>
        IGameServer CreateServer(IPlayer[] players, string parameters);
    }

    public class NewAgentCreatingEventArgs : EventArgs
    {
        public string Name { get; set; }
    }

    public interface IServerForm : IForm
    {
        event EventHandler<NewAgentCreatingEventArgs> NewAgentCreating;

    }
}
