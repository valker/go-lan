using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Valker.PlayServer
{



    public class Server
    {

        TcpListener listener = new TcpListener();
        public ServerConfig Config { get; set; }
        public Server(ServerConfig config) { Config = config; }
        public void Start() 
        {
            listener.
        }


    }
}
