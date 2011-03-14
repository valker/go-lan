using System;
using System.Xml.Serialization;
using Valker.PlayOnLan.Api.Communication;

namespace Valker.PlayOnLan.Server.Messages.Server
{
    public class AcceptNewPartyMessage : ServerMessage
    {
        private static XmlSerializer Serializer = new XmlSerializer(typeof(ServerMessage), new[]{typeof(AcceptNewPartyMessage)});

        private string _requesterName;

        public string RequesterName
        {
            get { return _requesterName; }
            set { _requesterName = value; }
        }
        private string _gameType;

        public string GameType
        {
            get { return _gameType; }
            set { _gameType = value; }
        }
        private string _accepterName;

        public string AccepterName
        {
            get { return _accepterName; }
            set { _accepterName = value; }
        }

        public AcceptNewPartyMessage()
        {

        }

        public AcceptNewPartyMessage(string requesterName, string gameType, string accepterName)
        {
            // TODO: Complete member initialization
            this._requesterName = requesterName;
            this._gameType = gameType;
            this._accepterName = accepterName;
        }
        #region Overrides of ServerMessage

        public override void Execute(IServerMessageExecuter server, IClientInfo sender)
        {
            if (server == null) throw new ArgumentNullException();
            server.AcceptPartyRequest(RequesterName, GameType, AccepterName);
        }

        #endregion

        protected override XmlSerializer GetSerializer()
        {
            return Serializer;
        }
    }
}
