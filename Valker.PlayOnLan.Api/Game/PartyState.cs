using System;
using System.Linq;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Api.Game
{
    public class PartyState
    {
        public PartyState()
        {
            
        }

        public int PartyId { get; set; }

        public PartyStatus Status { get; set; }

        private IPlayer[] _players;

        [XmlIgnore]
        public IPlayer[] Players { 
            get 
            {
                return this._players;
            } 
            set 
            {
                if(value == null) throw new ArgumentNullException();
                this._players = value;
                this.Names = this._players.Select(delegate(IPlayer p)
                {
                    if (p == null)
                    {
                        throw new InvalidOperationException();
                    }

                    return p.PlayerName;
                }).ToArray();
            } 
        }

        public string[] Names { get; set; }

        public string GameTypeId { get; set; }

        public string Parameters { get; set; }

        public void Dispose()
        {
            // todo : implement notification of players
        }

        [XmlIgnore]
        public IGameServer Server { get; set; }
    }
}