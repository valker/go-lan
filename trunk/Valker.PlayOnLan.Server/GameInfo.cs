using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server
{
    public class GameInfo
    {
        public GameInfo(string gameName, IMessageConnector connector)
        {
            var gameId = gameName.Split(',');
            this.GameName = gameId[0];
            this.GameId = gameId[1];
            this.Connector = connector;
        }

        public IMessageConnector Connector { get; set; }

        private string GameName { get; set; }

        public string GameId { get; set; }

        public override string ToString()
        {
            return this.GameName + " - " + this.Connector.Name;
        }
    }
}