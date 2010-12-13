using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Valker.PlayServer
{



    public class Server
    {

        // todo: setup port
        TcpListener listener = new TcpListener(1);
        public ServerConfig Config { get; set; }
        public Server(ServerConfig config) { Config = config; }
        public void Start() 
        {
            //listener.
        }


    }
}
