/*
 * Сделано в SharpDevelop.
 * Пользователь: valker
 * Дата: 23.02.2011
 * Время: 8:40
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using Valker.PlayOnLan.Api.Game;

namespace Valker.TicTacToePlugin
{
    /// <summary>
    /// Description of MyClass.
    /// </summary>
    public class TicTacToeGame : IGameType
    {
        
        public override string ToString() {
            return Name + ',' + Id;
        }

        /// <summary>
        /// Describes the name of the game on original language
        /// </summary>
        public string Name {
            get {
                return "Tic-tac-toe";
            }
        }

        /// <summary>
        /// Creates client component of the game
        /// </summary>
        /// <returns></returns>
        public IGameClient CreateClient(IForm parent)
        {
            return new TicTacToeClient(parent);
        }

        /// <summary>
        /// Creates server component of the game
        /// </summary>
        /// <param name="players">array of player, which will participate</param>
        /// <param name="parameters">parameters of the game depended on the game type</param>
        /// <returns>the server componend of the game</returns>
        public IGameServer CreateServer(IPlayer[] players, string parameters)
        {
            return new TicTacToeServer(players, parameters);
        }

        /// <summary>
        /// Ask user about parameters of the game. It depends on the game type
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public string AskParam(IForm parent)
        {
            return new TicTacToeParameters(3, 3).ToString();
//            var f = new ParametersForm();
//            var r = f.ShowDialog(parent);
//            return r == DialogResult.OK ? f.Parameters.ToString() : "";
        }

        /// <summary>
        /// Defines the unique identifier of the game type
        /// </summary>
        public string Id {
            get {
                return "161DB3D0-94C3-44b7-AA60-ED0061706D74";
            }
        }
    }
}