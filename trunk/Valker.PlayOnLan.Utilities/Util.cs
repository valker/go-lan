using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api;

namespace Valker.PlayOnLan.Utilities
{
    public class Util
    {
        public static string[] ExtractParams(string message)
        {
            int left = message.IndexOf('[');
            int right = message.IndexOf(']');
            var part = message.Substring(left + 1, right - (left + 1));
            return part.Split(',');
        }

        public static string ExtractCommand(string message)
        {
            var indexOf = message.IndexOf('[');
            return indexOf == -1 ? message : message.Substring(0, indexOf);
        }

        public static Stone Opposite(Stone stone)
        {
            if (stone==Stone.Black)
            {
                return Stone.White;
            }
            else if (stone==Stone.White)
            {
                return Stone.Black;
            }else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
