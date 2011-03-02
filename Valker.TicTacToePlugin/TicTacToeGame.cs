/*
 * Сделано в SharpDevelop.
 * Пользователь: valker
 * Дата: 23.02.2011
 * Время: 8:40
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.Generic;

namespace Valker.TicTacToePlugin
{
    /// <summary>
    /// Description of MyClass.
    /// </summary>
    public class TicTacToeGame : Valker.PlayOnLan.Api.Game.IGameType
    {
        
        public override string ToString() {
            return Name + ',' + ID;
        }

        public string Name {
            get {
                return "Крестики-нолики";
            }
        }
        
        public Valker.PlayOnLan.Api.Game.IGameServer CreateServer()
        {
            return new TicTacToeServer(this);
        }
        
        public Valker.PlayOnLan.Api.Game.IGameClient CreateClient()
        {
            throw new NotImplementedException();
        }
        
        public string ID {
            get {
                return "161DB3D0-94C3-44b7-AA60-ED0061706D74";
            }
        }
    }
}