using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Valker.PlayOnLan.Api.Game;
using System.Diagnostics;
using log4net;

namespace Valker.PlayOnLan.PluginLoader
{
	public static class Loader
	{

	    private static readonly ILog Log = LogManager.GetLogger(typeof (Loader));

		public static IGameType[] Load (string path)
		{
			var directoryInfo = new DirectoryInfo (path);
			FileInfo[] files = directoryInfo.GetFiles ("*.dll", SearchOption.TopDirectoryOnly);
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
                Log.Warn("cannot load plugin", e);
			}
			
			if (assembly == null)
				return null;
			
			Type[] types = assembly.GetTypes ();
			foreach (Type type in types) {
				if (typeof(IGameType).IsAssignableFrom (type) && !type.IsInterface && !type.IsAbstract) {
					var gameType = (IGameType)Activator.CreateInstance (type);
					return gameType;
				}
			}
			
			return null;
		}

		private static IEnumerable<IGameType> GetTypes (IEnumerable<FileInfo> files)
		{
			string[] fileNames = files.Select (fi => fi.FullName).ToArray ();
			var gameTypes = new List<IGameType> ();
			
			foreach (string fileName in fileNames) {
				IGameType gameType = TryFindGameType (fileName);
				if (gameType != null)
					gameTypes.Add (gameType);
			}
			
			return gameTypes;
		}
	}
}
