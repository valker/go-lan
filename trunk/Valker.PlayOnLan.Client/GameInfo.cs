using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Client
{
    internal class GameInfo
    {
        public override string ToString()
        {
            return this.GameName + " - " + this.Connector.Name;
        }

        public GameInfo(string gameName, IMessageConnector connector)
        {
            this.GameName = gameName;
            this.Connector = connector;
        }

        private IMessageConnector Connector { get; set; }

        private string GameName { get; set; }
    }
}