using System;
using System.Collections.Generic;
using System.Linq;

namespace Valker.PlayOnLan.GoPlugin
{
    public class ParamsEventArgs : EventArgs
    {
        private readonly Dictionary<string, string> _dict = new Dictionary<string, string>();

        public ParamsEventArgs(IEnumerable<string> strings)
        {
            foreach (var s in strings)
            {
                var kv = s.Split('=');
                if (kv.Length != 2)
                {
                    throw new InvalidOperationException("wrong param " + s);
                }

                _dict.Add(kv[0],kv[1]);
            }
        }

        public string[] Keys { get { return _dict.Keys.ToArray(); } }

        public string this [string key]
        {
            get { return _dict[key]; }
        }

        public bool TryGetValue(string key, out string value)
        {
            return _dict.TryGetValue(key, out value);
        }
    }
}