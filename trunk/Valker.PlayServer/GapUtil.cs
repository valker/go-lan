using System.Text;

namespace Valker.PlayServer
{
    public static class GapUtil
    {
        public static void WriteAsync(this IClient gapClient, string message)
        {
            gapClient.WriteAsync(Encoding.ASCII.GetBytes(message));
        }

        public static void WriteAsyncAndDispose(this IClient gapClient, string message)
        {
            gapClient.WriteAsyncAndDispose(Encoding.ASCII.GetBytes(message));
        }
    }
}