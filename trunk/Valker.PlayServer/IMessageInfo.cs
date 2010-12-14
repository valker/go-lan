namespace Valker.PlayServer
{
    internal interface IMessageInfo
    {
        bool Disposing { get; set; }

        byte[] Value { get; set; }
    }
}