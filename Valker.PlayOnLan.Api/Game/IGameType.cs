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
		string Name {get;}
		string ID {get;}
		IGameServer CreateServer();
		IGameClient CreateClient();
	}
	
	public interface IGameServer{}

	public interface IGameClient{}
	
}
