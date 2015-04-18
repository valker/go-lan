using System;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server
{
    public class GameInfo
    {
        public GameInfo(string gameName, IMessageConnector connector)
        {
            if (gameName == null) throw new ArgumentNullException();
            if (connector == null) throw new ArgumentNullException();
            var gameId = gameName.Split(',');
            if (gameId.Length < 2) throw new ArgumentException();
            GameName = gameId[0];
            GameTypeId = gameId[1];
            Connector = connector;
        }

        public IMessageConnector Connector { get; set; }

        private string GameName { get; set; }

        public string GameTypeId { get; set; }

        public override string ToString()
        {
            return GameName;
        }
    }
}