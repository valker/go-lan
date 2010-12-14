namespace Valker.PlayServer
{
    internal interface IIdentifiedClient
    {
        IClient Client { get; }

        string ID { get; }
    }
}