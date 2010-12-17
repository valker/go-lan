using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

namespace Valker.PlayServer
{
    public class Listener : IListener
    {
        // List of rooms
        private List<IRoom> _rooms = new List<IRoom>();
        // List of clients
        private List<IClient> _clients = new List<IClient>();

//        private readonly ICollection<IIdentifiedClient> _announcers = new List<IIdentifiedClient>();
        private readonly ICommandReceiver _commandReceiver = new CommandReceiver();
        private readonly IConnectionEstablisher _connectionEstablisher = new ConnectionEstablisher();
//        private readonly IListenerCollection _listenersClients = new ListenerCollection();
//        private readonly List<IPassThroughConnection> _passThroughConnections = new List<IPassThroughConnection>();
//        private readonly DateTime _startTime = DateTime.Now;

//        private readonly Dictionary<string, ExecuteCommandDelegate> messageHandlers =
//            new Dictionary<string, ExecuteCommandDelegate>();

        public Listener()
        {
            Port = 1000;
            _connectionEstablisher.ConnectionEstablished +=
                OnConnectionEstablished;
            _commandReceiver.MessageReceived += CommandReceiverOnMessageReceived;

            _rooms.Add(new Room("Main Room"));
//            messageHandlers.Add("LISTEN", CreateListenerClient);
//            messageHandlers.Add("ANNOUNCE", CreateAnnouncerClient);
//            messageHandlers.Add("ACQUIRE", CreateAcquirerClient);
//            messageHandlers.Add("REPORT", ReportClients);
//            messageHandlers.Add("STATUS", ReportStatus);
        }

        public int Port { get; set; }

        #region IListener Members

        public void Start()
        {
            if ((Port >= 0) && (Port <= 0xffff))
            {
                _connectionEstablisher.Start(Port);
            }
            else
            {
                Debug.WriteLine("Wrong port number used");
            }
        }

        #endregion

//        private void ClosePassthrough(IPassThroughConnection passThroughConnection)
//        {
//            _listenersClients.SendMessage("103 DISCONNECTED " + passThroughConnection.Announcer.ID);
//            lock (_passThroughConnections)
//            {
//                _passThroughConnections.Remove(passThroughConnection);
//            }
//        }

        private void CommandReceiverOnMessageReceived(object sender, EventArgs args)
        {
            var args2 = (MessageReceivedEventArgs) args;
            var message = args2.Message;
            var client = args2.Client;

            //XmlSerializer xmlSerializer = new XmlSerializer(XmlTypeMapping());

//            string key = args2.Message.Split(new char[] {' '})[0];
//            if (messageHandlers.TryGetValue(key, out delegate2))
//            {
//                delegate2(args2);
//            }
//            else
//            {
//                args2.Client.WriteAsyncAndDispose("402 BAD COMMAND\r\n");
//            }
        }

/*
        private void CreateAcquirerClient(MessageReceivedEventArgs args)
        {
            Func<IPassThroughConnection, bool> predicate = null;
            IClient gapClient = args.Client;
            string id = GetId(args.Message);
            if (id.Length == 0)
            {
                gapClient.WriteAsyncAndDispose("402 BAD COMMAND\r\n");
            }
            else
            {
                IIdentifiedClient announcer = ExtractAnnouncerByID(id);
                if (announcer != null)
                {
//                    gapClient.ClientType = ClientType.Acquirer;
//                    announcer.Client.ClientType = ClientType.Announcer;
                    gapClient.WriteAsync("200 OK\r\n");
                    _listenersClients.SendMessage("102 ACQUIRED " + id);
                    IPassThroughConnection item = new PassThroughConnection(announcer, gapClient);
                    lock (_passThroughConnections)
                    {
                        _passThroughConnections.Add(item);
                    }
                    item.Closed += new EventHandler(PassthroughClosed);
                    item.Start();
                }
                else
                {
                    IPassThroughConnection[] connectionArray;
                    lock (_passThroughConnections)
                    {
                        connectionArray = _passThroughConnections.ToArray();
                    }
                    if (predicate == null)
                    {
                        predicate =
                            delegate(IPassThroughConnection connection) { return connection.Announcer.ID == id; };
                    }
                    if (connectionArray.FirstOrDefault<IPassThroughConnection>(predicate) != null)
                    {
                        gapClient.WriteAsync("401 BUSY\r\n");
                    }
                    else
                    {
                        gapClient.WriteAsync("404 NOTFOUND\r\n");
                    }
                }
            }
        }
*/

/*
        private void CreateAnnouncerClient(MessageReceivedEventArgs args)
        {
            IClient gapClient = args.Client;
            string id = GetId(args.Message);
            if (id.Length == 0)
            {
                gapClient.WriteAsyncAndDispose("402 BAD COMMAND\r\n");
            }
            else
            {
                gapClient.WriteAsync("200 OK\r\n");
                lock (_announcers)
                {
                    _announcers.Add(new AnnouncerInfo(id, gapClient));
                }
                _listenersClients.SendMessage("101 ANNOUNCE " + id);
            }
        }
*/

/*
        private void CreateListenerClient(MessageReceivedEventArgs args)
        {
            IClient gapClient = args.Client;
            gapClient.WriteAsync("200 OK\r\n");
            lock (_listenersClients)
            {
                _listenersClients.Add(gapClient);
            }
        }
*/

/*
        private IIdentifiedClient ExtractAnnouncerByID(string id)
        {
            IIdentifiedClient client;
            Func<IIdentifiedClient, bool> predicate = null;
            lock (_announcers)
            {
                if (predicate == null)
                {
                    predicate = delegate(IIdentifiedClient info) { return info.ID == id; };
                }
                client = _announcers.FirstOrDefault<IIdentifiedClient>(predicate);
                if (client != null)
                {
                    _announcers.Remove(client);
                }
            }
            return client;
        }
*/

/*
        private static string GetId(string message)
        {
            string[] strArray = message.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length < 2)
            {
                return string.Empty;
            }
            return strArray[1];
        }
*/

        private void OnConnectionEstablished(object sender, ConnectionEstablishedEventArgs args)
        {
            var client = new Client(args.TcpClient);
            _commandReceiver.Start(client);
        }

/*
        private void PassthroughClosed(object sender, EventArgs e)
        {
            var passThroughConnection = (IPassThroughConnection) sender;
            ClosePassthrough(passThroughConnection);
        }
*/

/*
        private void ReportClients(MessageReceivedEventArgs args)
        {
            string message =
                string.Join(string.Empty,
                            _announcers.Select<IIdentifiedClient, string>(
                                delegate(IIdentifiedClient info) { return ("101 ANNOUNCE " + info.ID.ToString() + "\r\n"); }).ToArray<string>()) +
                string.Join(string.Empty,
                            _passThroughConnections.Select<IPassThroughConnection, string>(
                                delegate(IPassThroughConnection connection) { return ("102 ACQUIRED " + connection.Announcer.ID + "\r\n"); }).ToArray<string>());
            args.Client.WriteAsyncAndDispose(message);
        }
*/

/*
        private void ReportStatus(MessageReceivedEventArgs args)
        {
            string message =
                string.Concat(new object[]
                                  {
                                      "110 ", (TimeSpan) (DateTime.Now - _startTime), "\r\n111 ", GC.GetTotalMemory(false),
                                      "\r\n"
                                  });
            args.Client.WriteAsyncAndDispose(message);
        }
*/

    }
}