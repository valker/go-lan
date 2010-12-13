using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Valker.Api;

namespace NetworkConnect.Notification
{
    public class Sender : ILoggable
    {
        public Sender(string name)
        {
            Name = name;
        }

        protected string Name { get; set; }

        public void Run()
        {
            var endPoint = new IPEndPoint(IPAddress.Loopback, 9050);
            using (var client = new UdpClient(){EnableBroadcast = true})
            {
                var dgram = Encoding.ASCII.GetBytes("Hello");
                Log("before sending");
                int sent = client.Send(dgram, dgram.Length, endPoint);
                Log("after sending");
                while (true)
                {
                    
                }
            }
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
