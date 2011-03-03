using System.Windows.Forms;
using Valker.PlayOnLan.Api.Communication;
using Valker.PlayOnLan.Client.Communication;

namespace Valker.PlayOnLan.Client
{
    internal class Program
    {
        private static void Main()
        {
            // local server
            var server = new Server.ServerImpl(new IMessageConnector[0]);

            // test form for local servers
            Form form = new ServerForm(server);
            Application.Run(form);
        }
    }
}