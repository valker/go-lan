using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Valker.PlayServer
{
    public class Client : IClient, IDisposable
    {
        private const int BufferSize = 0x100;
        private readonly byte[] _buffer = new byte[0x100];
        private readonly LinkedList<IMessageInfo> _messagesToSent = new LinkedList<IMessageInfo>();
        private bool _disposed;

        public Client(TcpClient tcpClient)
        {
            StringBuilder = new StringBuilder();
            TcpClient = tcpClient;
        }

        private TcpClient TcpClient { get; set; }

        private StringBuilder StringBuilder { get; set; }

        #region IClient Members

        public event EventHandler Disconnected;

        public event EventHandler<ReadCompletedEventArgs> ReadCompleted;

        public void AppendMessageFromBuffer(int bytesRead)
        {
            string str = Encoding.ASCII.GetString(_buffer, 0, bytesRead);
            StringBuilder.Append(str);
        }

        public void Dispose()
        {
            lock (this)
            {
                if (!_disposed)
                {
                    _disposed = true;
                    Disposing(true);
                    GC.SuppressFinalize(this);
                }
            }
        }

        public string GetMessage()
        {
            return StringBuilder.ToString();
        }

        public void ReadAsync()
        {
            lock (TcpClient)
            {
                if (TcpClient.Connected)
                {
                    try
                    {
                        TcpClient.GetStream().BeginRead(_buffer, 0, 0x100, ReadCallback, null);
                        return;
                    }
                    catch (IOException exception)
                    {
                        Debug.WriteLine(exception.ToString());
                    }
                    catch (ObjectDisposedException exception2)
                    {
                        Debug.WriteLine(exception2.ToString());
                    }
                }
            }
            InvokeDisconnected(EventArgs.Empty);
        }

        public void WriteAsync(byte[] value)
        {
            var info = new MessageInfo(value, false);
            WriteMessage(info);
        }

        public void WriteAsyncAndDispose(byte[] value)
        {
            var info = new MessageInfo(value, true);
            WriteMessage(info);
        }

        #endregion

        private void BeginWrite(IMessageInfo value)
        {
            try
            {
                TcpClient.GetStream().BeginWrite(value.Value, 0, value.Value.Length,
                                              value.Disposing
                                                  ? WriteCallbackAndDispose
                                                  : new AsyncCallback(WriteCallback), this);
                return;
            }
            catch (IOException exception)
            {
                Debug.WriteLine(exception.ToString());
            }
            catch (ObjectDisposedException exception2)
            {
                Debug.WriteLine(exception2.ToString());
            }
            InvokeDisconnected(EventArgs.Empty);
        }

        private void Disposing(bool disposing)
        {
            if (disposing)
            {
                TcpClient.GetStream().Close();
                TcpClient.Close();
            }
        }

        private void InvokeDisconnected(EventArgs e)
        {
            EventHandler disconnected;
            lock (this)
            {
                disconnected = Disconnected;
            }
            if (disconnected != null)
            {
                disconnected(this, e);
            }
        }

        private void InvokeReadCompleted(ReadCompletedEventArgs e)
        {
            EventHandler<ReadCompletedEventArgs> readCompleted;
            lock (this)
            {
                readCompleted = ReadCompleted;
            }
            if (readCompleted != null)
            {
                readCompleted(this, e);
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            try
            {
                int num;
                try
                {
                    num = TcpClient.GetStream().EndRead(ar);
                }
                catch (IOException exception)
                {
                    Debug.WriteLine(exception.ToString());
                    InvokeDisconnected(EventArgs.Empty);
                    return;
                }
                catch (ObjectDisposedException exception2)
                {
                    Debug.WriteLine(exception2.ToString());
                    InvokeDisconnected(EventArgs.Empty);
                    return;
                }
                if (num == 0)
                {
                    InvokeDisconnected(EventArgs.Empty);
                }
                else
                {
                    var e = new ReadCompletedEventArgs();
                    e.Buffer = _buffer;
                    e.BytesRead = num;
                    InvokeReadCompleted(e);
                }
            }
            catch (Exception exception3)
            {
                Debug.WriteLine(exception3.ToString());
            }
        }

        public void WriteAsync(string value)
        {
            WriteAsync(Encoding.ASCII.GetBytes(value));
        }

        public void WriteAsyncAndDispose(string value)
        {
            WriteAsyncAndDispose(Encoding.ASCII.GetBytes(value));
        }

        private void WriteCallback(IAsyncResult asyncResult)
        {
            try
            {
                TcpClient.GetStream().EndWrite(asyncResult);
                lock (_messagesToSent)
                {
                    if (_messagesToSent.Count != 0)
                    {
                        IMessageInfo info = _messagesToSent.First.Value;
                        _messagesToSent.RemoveFirst();
                        BeginWrite(info);
                    }
                }
            }
            catch (IOException exception)
            {
                Debug.WriteLine(exception.ToString());
                InvokeDisconnected(EventArgs.Empty);
            }
            catch (ObjectDisposedException exception2)
            {
                Debug.WriteLine(exception2.ToString());
                InvokeDisconnected(EventArgs.Empty);
            }
            catch (Exception exception3)
            {
                Debug.WriteLine(exception3.ToString());
            }
        }

        private void WriteCallbackAndDispose(IAsyncResult asyncResult)
        {
            WriteCallback(asyncResult);
            try
            {
                TcpClient.Close();
            }
            catch (Exception)
            {
            }
        }

        private void WriteMessage(IMessageInfo info)
        {
            lock (_messagesToSent)
            {
                if (_messagesToSent.Count == 0)
                {
                    BeginWrite(info);
                }
                else
                {
                    _messagesToSent.AddLast(info);
                }
            }
        }
    }
}