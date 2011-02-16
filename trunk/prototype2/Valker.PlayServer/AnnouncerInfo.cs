namespace Valker.PlayServer
{
    internal class AnnouncerInfo : IIdentifiedClient
    {
        public AnnouncerInfo(string id, IClient client)
        {
            ID = id;
            Client = client;
        }

        #region IIdentifiedClient Members

        public IClient Client { get; set; }

        public string ID { get; set; }

        #endregion
    }
}