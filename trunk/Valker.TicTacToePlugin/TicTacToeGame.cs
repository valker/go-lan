﻿/*
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

        public string Name {
            get {
                return "Tic-tac-toe";
            }
        }
        
        public IGameClient CreateClient(IForm parent)
        {
            return new TicTacToeClient(parent);
        }

        public IGameServer CreateServer(IPlayer[] players, string parameters)
        {
            return new TicTacToeServer(players, parameters);
        }

        public string AskParam(IForm parent)
        {
            return new TicTacToeParameters(3, 3).ToString();
//            var f = new ParametersForm();
//            var r = f.ShowDialog(parent);
//            return r == DialogResult.OK ? f.Parameters.ToString() : "";
        }

        public string Id {
            get {
                return "161DB3D0-94C3-44b7-AA60-ED0061706D74";
            }
        }
    }
}