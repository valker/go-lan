﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.PluginLoader
{
    public static class Loader
    {
        public static IGameType[] Load(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            var files = directoryInfo.GetFiles("*.dll", SearchOption.TopDirectoryOnly);
            return GetTypes(files).ToArray();
        }

        private static IEnumerable<IGameType> GetTypes(IEnumerable<FileInfo> files)
        {
            foreach (var fileInfo in files)
            {
                var assembly = Assembly.LoadFile(fileInfo.FullName);
                if(assembly == null) continue;

                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (typeof(IGameType).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        var gameType = (IGameType) Activator.CreateInstance(type);
                        yield return gameType;
                    }
                }
            }
        }
    }
}