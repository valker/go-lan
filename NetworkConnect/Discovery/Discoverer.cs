using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Valker.Api;

namespace NetworkConnect.Discovery
{
    public class Discoverer : IDisposable, ILoggable
    {
        private const string Request = "<REQUEST/>";
        private const int DefaultClientPort = 19050;
        private const int DefaultServerPort = 19051;

        private UdpClient _client;
        private int _port = DefaultClientPort;
        private Timer _timer;
        private bool _server;

        public Discoverer(string name, bool server)
        {
            _server = server;
            Name = name;

            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            for (
                int port = server ? DefaultServerPort : DefaultClientPort; 
                port < IPEndPoint.MaxPort; 
                ++port)
                {
                    try
                    {
                        ep = new IPEndPoint(IPAddress.Loopback, port);
                        _client = new UdpClient(ep) { EnableBroadcast = server ? false : true };
                        _port = port;
                        break;

                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            if (_client == null) {
                throw new NullReferenceException("socket cannot be created");
            }

        }

        protected string Name { get; set; }

        public void StartListener(bool server)
        {
            _server = server;
            Log("starting listening on port " + _port);
            _client.BeginReceive(ReceiveCallback, null);
            if (server)
            {
                _timer = new Timer(Callback, null, 10000, 10000);
            }
        }

        private void Callback(object state)
        {
            SendRequest();
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            Log("listening callback");
            IPEndPoint ep = new IPEndPoint(IPAddress.Loopback, 0);
            byte[] buffer = null;
            try
            {
                buffer = _client.EndReceive(result, ref ep);

            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            } 
            string message = Encoding.UTF8.GetString(buffer);
            ProcessRequest(message, ep);
            StartListener(_server);
        }

        private void ProcessRequest(string message, IPEndPoint point)
        {
            Log("process request");
            if (message == Request)
            {
                SendReply(point);
            } else
            {
                Log(message);
            }
        }

        public void SendRequest()
        {
            var message = Request;
            byte[] datagram = Encoding.UTF8.GetBytes(message);
            for (int i = 0; i < 5; ++i )
            {
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, DefaultClientPort + i);
                Log("sending request on port " + endpoint.Port);
                _client.BeginSend(datagram, datagram.Length, endpoint, SendRequestCallback, null);
            }
        }

        private void SendRequestCallback(IAsyncResult result)
        {
            try
            {
                var v = _client.EndSend(result);
                Log("request sent : " + v);
            }
            catch(Exception ex)
            {
                Log(ex.ToString());
            }
        }

        private void SendReply(IPEndPoint point)
        {
            Log("sending reply");
            var message = "<Name>" + Name + "</Name>";
            byte[] datagram = Encoding.UTF8.GetBytes(message);
            _client.BeginSend(datagram, datagram.Length, point, SendReplyCallback, null);
        }

        private void SendReplyCallback(IAsyncResult ar)
        {
            try
            {
                var v = _client.EndSend(ar);
                Log("reply sent " + v);

            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            }
        }

        public void Dispose()
        {
            Log("disposing");
            _client.Close();
        }

        public event EventHandler<OnMessageEventArgs> OnMessage;

        private void InvokeOnMessage(OnMessageEventArgs e)
        {
            EventHandler<OnMessageEventArgs> handler = OnMessage;
            if (handler != null) handler(this, e);
        }

        private void Log(string message)
        {
            InvokeOnMessage(new OnMessageEventArgs(){Message = message});
        }
    }
}
