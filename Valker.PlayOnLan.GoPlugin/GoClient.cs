using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Api.Game;
using Valker.PlayOnLan.Utilities;

namespace Valker.PlayOnLan.GoPlugin
{
    /// <summary>
    /// Implement UI independent client part of plugin
    /// </summary>
    public class GoClient : IGameClient
    {
        #region IGameClient Members

        /// <summary>
        /// Create the form that will allow to user to play the game
        /// </summary>
        /// <param name="parameters">parameters of the party</param>
        /// <param name="playerName">name of player</param>
        /// <param name="gui">name of gui that is user for current session</param>
        /// <returns>playing form object</returns>
        public IPlayingForm CreatePlayingForm(string parameters, string playerName, string gui)
        {
            IPlayingForm form = null;

            IInfo[] plugins = GetPlayingFormsPlugins().Where(info => info.Gui == gui).ToArray();
            if (plugins.Length == 1)
            {
                form = plugins[0].CreatePlayingForm(parameters, playerName, this);
            }

            return form;
        }

        public void ExecuteMessage(string message)
        {
            switch (Util.ExtractCommand(message))
            {
                case "ALLOW":
                    Allow(this, EventArgs.Empty);
                    break;
                case "WAIT":
                    Wait(this, EventArgs.Empty);
                    break;
                case "MSG":
                    ShowMessage(this, new ShowMessageEventArgs(Util.ExtractParams(message)));
                    break;
                case "FIELD":
                    FieldChanged(this, new CellChangedEventArgs(Util.ExtractParams(message)));
                    break;
                case "MARK":
                    Mark(this, EventArgs.Empty);
                    break;
                case "PARAMS":
                    Params(this, new ParamsEventArgs(Util.ExtractParams(message)));
                    break;
                case "EATED":
                    Eated(this, new EatedEventArgs(Util.ExtractParams(message)));
                    break;
                default:
                    throw new InvalidOperationException("wrong message "
                                                        + message);
            }
        }

        public event EventHandler Closed;

        public event EventHandler<MessageEventArgs> OnMessageReady;

        #endregion

        public event EventHandler Allow = delegate { };

        public event EventHandler Wait = delegate { };

        public event EventHandler Mark = delegate { };

        public event EventHandler<ParamsEventArgs> Params = delegate { };

        public event EventHandler<EatedEventArgs> Eated = delegate { };

        public event EventHandler<CellChangedEventArgs> FieldChanged = delegate { };

        public event EventHandler<ShowMessageEventArgs> ShowMessage = delegate { };

        public void Click(int x, int y)
        {
            SendMessageToServer(string.Format("MOVE[{0},{1}]", x, y));
        }

        private void SendMessageToServer(string text)
        {
            OnMessageReady(this, new MessageEventArgs("", "_server", text));
        }

        private static IEnumerable<IInfo> GetPlayingFormsPlugins()
        {
            string path = Environment.CurrentDirectory;
            var directoryInfo = new DirectoryInfo(path);
            FileInfo[] files = directoryInfo.GetFiles("*.dll", SearchOption.TopDirectoryOnly);

            foreach (string fileInfo in files.Select(fi => fi.FullName))
            {
                string fileName = fileInfo;
                Assembly assembly = Assembly.LoadFile(fileName);
                if (assembly == null) continue;

                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (typeof (IInfo).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        var gameType = (IInfo) Activator.CreateInstance(type);
                        yield return gameType;
                    }
                }
            }
        }

        public void Pass()
        {
            SendMessageToServer("PASS");
        }
    }
}