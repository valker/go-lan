namespace Valker.PlayOnLan.Server
{
    internal class PartyRequest
    {
        public PartyRequest(string name, ServerGameInfo info)
        {
            this.Name = name;
            this.Info = info;
        }

        protected ServerGameInfo Info { get; set; }

        public string Name { get; set; }
    }
}