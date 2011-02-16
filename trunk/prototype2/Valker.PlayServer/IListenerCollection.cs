namespace Valker.PlayServer
{
    internal interface IListenerCollection
    {
        void Add(IClient item);
        void SendMessage(string message);
    }
}