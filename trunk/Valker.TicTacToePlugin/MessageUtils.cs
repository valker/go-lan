namespace Valker.TicTacToePlugin
{
    internal static class MessageUtils
    {
        public static string[] ExtractParams(string message)
        {
            int left = message.IndexOf('<');
            int right = message.IndexOf('>');
            var part = message.Substring(left + 1, right - (left + 1));
            return part.Split(',');
        }

        public static string ExtractCommand(string message)
        {
            var indexOf = message.IndexOf('<');
            return indexOf == -1 ? message : message.Substring(0, indexOf);
        }
    }
}
