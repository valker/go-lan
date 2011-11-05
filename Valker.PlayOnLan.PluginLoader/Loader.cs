using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Valker.PlayOnLan.Api.Game;
using System.Diagnostics;

namespace Valker.PlayOnLan.PluginLoader
{
	public static class Loader
	{
		public static IGameType[] Load (string path)
		{
			var directoryInfo = new DirectoryInfo (path);
			var files = directoryInfo.GetFiles ("*.dll", SearchOption.TopDirectoryOnly);
			return GetTypes (files).ToArray ();
		}

		/// <summary>
		/// TODO: write a comment.
		/// </summary>
		/// <param name="fileName"> A string </param>
		private static IGameType TryFindGameType (string fileName)
		{
			Trace.WriteLine (fileName);
			Assembly assembly = null;
			try {
				assembly = Assembly.LoadFile (fileName);
			} catch (Exception e) {
				// skip any errors
			}
			
			if (assembly == null)
				return null;
			
			var types = assembly.GetTypes ();
			foreach (var type in types) {
				if (typeof(IGameType).IsAssignableFrom (type) && !type.IsInterface && !type.IsAbstract) {
					var gameType = (IGameType)Activator.CreateInstance (type);
					return gameType;
				}
			}
			
			return null;
		}

		private static IEnumerable<IGameType> GetTypes (IEnumerable<FileInfo> files)
		{
			var fileNames = files.Select (fi => fi.FullName).ToArray ();
			List<IGameType> gameTypes = new List<IGameType> ();
			
			foreach (var fileName in fileNames) {
				var gameType = TryFindGameType (fileName);
				if (gameType != null)
					gameTypes.Add (gameType);
			}
			
			return gameTypes;
		}
	}
}
