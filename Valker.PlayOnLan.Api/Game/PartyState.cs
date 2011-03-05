using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Game;

namespace Valker.PlayOnLan.Api.Communication
{
    public class PartyState
    {
        public PartyState()
        {
            
        }

        public PartyStatus Status { get; set; }

        private IPlayer[] _players;

        [XmlIgnore]
        public IPlayer[] Players { 
            get 
            {
                return _players;
            } 
            set 
            {
                _players = value;
                Names = _players.Select(p => p.PlayerName).ToArray();
            } 
        }

        public string[] Names { get; set; }

        public string GameTypeId { get; set; }

        public void Dispose()
        {
            // todo : implement notification of players
        }

        [XmlIgnore]
        public Game.IGameServer Server { get; set; }
    }
}
