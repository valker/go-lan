using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.GoPlugin
{
    public class GoClient : IGameClient
    {
        public GoClient(IForm parent)
        {
            Parent = parent;
        }

        protected IForm Parent { get; set; }

        public IPlayingForm CreatePlayingForm(string parameters, string playerName, string gui)
        {
            IPlayingForm form = null;

            if (gui == "winforms")
            {
                var plugins = GetPlayingFormsPlugins().ToArray();
                if(plugins.Length == 0) return null;

                form = plugins[0].CreatePlayingForm(parameters, playerName);
            }

            return form;
        }

        private static IEnumerable<IInfo> GetPlayingFormsPlugins()
        {
            var path = Environment.CurrentDirectory;
            var directoryInfo = new DirectoryInfo(path);
            var files = directoryInfo.GetFiles("*.dll", SearchOption.TopDirectoryOnly);

            foreach (var fileInfo in files.Select(fi => fi.FullName))
            {
                string fileName = fileInfo;
                var assembly = Assembly.LoadFile(fileName);
                if (assembly == null) continue;

                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (typeof(IInfo).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        var gameType = (IInfo)Activator.CreateInstance(type);
                        yield return gameType;
                    }
                }
            }
        }

        public void ExecuteMessage(string message)
        {
            throw new NotImplementedException();
        }

        public event EventHandler Closed;
        public event EventHandler<MessageEventArgs> OnMessageReady;
    }
}
