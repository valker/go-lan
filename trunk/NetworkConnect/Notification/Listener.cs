using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Utils;
using Valker.Api;

namespace NetworkConnect.Notification
{
    public class Listener : ILoggable
    {
        private IPEndPoint _endPoint;
        private Socket _socket;

        public Listener(string name)
        {
            Name = name;
        }

        protected string Name { get; set; }

        public void Run()
        {
            _endPoint = new IPEndPoint(IPAddress.Any, 9050);
            using (var client = new UdpClient(_endPoint){EnableBroadcast = true})
            {
                while (true)
                {
                    var remote = new IPEndPoint(IPAddress.Any, 0);
                    Log("Before receive");
                    var bytes = client.Receive(ref remote);
                    Log("Received:" + bytes.ToStr());
                    var dgram = Encoding.ASCII.GetBytes(Name);
                    client.Send(dgram, dgram.Length, remote);
                    Log("Responce sent:" + dgram.ToStr());
                }
            }
        }

        private void Log(string message)
        {
            InvokeOnMessage(new OnMessageEventArgs(){Message = message});
        }

        public event EventHandler<OnMessageEventArgs> OnMessage;

        private void InvokeOnMessage(OnMessageEventArgs e)
        {
            EventHandler<OnMessageEventArgs> handler = OnMessage;
            if (handler != null) handler(this, e);
        }
    }
}
