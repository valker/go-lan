using System;

namespace Valker.PlayServer
{
    public interface IClient : IDisposable
    {
        event EventHandler Disconnected;

        event EventHandler<ReadCompletedEventArgs> ReadCompleted;

        void AppendMessageFromBuffer(int bytesRead);
        string GetMessage();
        void ReadAsync();
        void WriteAsync(byte[] value);
        void WriteAsyncAndDispose(byte[] value);
    }
}