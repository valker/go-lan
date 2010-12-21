using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq.agent;
using agsXMPP.protocol.iq.roster;
using NetworkConnect.Discovery;
using NetworkConnect.Notification;
using Valker.Api;

namespace Valker.PlayOnLan.Engine
{
    internal class GoLanEngine : Component, IEngine
    {
        private BackgroundWorker listenerWorker;
        private BackgroundWorker senderWorker;
        private IEnumerable<INeibour> _neibours = Enumerable.Empty<INeibour>();
        private Listener _listener;
        private Sender _sender;
        private XmppClientConnection _connection;

        public GoLanEngine()
        {
            Name = Guid.NewGuid().ToString();
            InitializeComponent();
            _connection = new XmppClientConnection();
            Jid jid = new Jid("admin@MOSDB9VF4J");
            _connection.Password = "12345";
            _connection.Username = jid.User;
            _connection.Server = jid.Server;
            _connection.AutoAgents = false;
            _connection.AutoPresence = true;
            _connection.AutoRoster = true;
            _connection.AutoResolveConnectServer = true;
            _connection.OnRosterStart += xmppCon_OnRosterStart;
            _connection.OnRosterItem += xmppCon_OnRosterItem;
            _connection.OnRosterEnd += xmppCon_OnRosterEnd;
            _connection.OnPresence += xmppCon_OnPresence;
            _connection.OnMessage += xmppCon_OnMessage;
            _connection.OnLogin += xmppCon_OnLogin;

            _connection.Open();

        }

        private void xmppCon_OnLogin(object sender)
        {
            Debug.WriteLine("Logged");
            _connection.Status = "ON GAME";
            _connection.SendMyPresence();
        }

        private void xmppCon_OnMessage(object sender, Message msg)
        {
            Debug.WriteLine(msg.Body);
        }

        private void xmppCon_OnPresence(object sender, Presence pres)
        {
            Debug.WriteLine(pres.Nickname + " : " + pres.Status);
        }

        private void xmppCon_OnRosterEnd(object sender)
        {
            Debug.WriteLine("<<<");
        }

        private void xmppCon_OnRosterItem(object sender, RosterItem item)
        {
            Debug.WriteLine(item.Jid);
        }

        private void xmppCon_OnRosterStart(object sender)
        {
            Debug.WriteLine(">>>");
        }


        public event EventHandler<NeiboursChangedEventArgs> NeiboursChanged;
        public string Name
        {
            get;
            set;
        }

        private void InvokeNeiboursChanged(NeiboursChangedEventArgs e)
        {
            EventHandler<NeiboursChangedEventArgs> handler = NeiboursChanged;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<OnMessageEventArgs> OnMessage;

        private void InvokeOnMessage(OnMessageEventArgs e)
        {
            EventHandler<OnMessageEventArgs> handler = OnMessage;
            if (handler != null) handler(this, e);
        }

        public IEnumerable<INeibour> Neibours
        {
            get { return _neibours; }
            set
            {
                var removed = _neibours.Except(value);
                var added = value.Except(_neibours);
                _neibours = value;
                foreach (INeibour neibour in removed)
                {
                    InvokeNeiboursChanged(new NeiboursChangedEventArgs(){Added = false,Neibour = neibour});
                }
                foreach (INeibour neibour in added)
                {
                    InvokeNeiboursChanged(new NeiboursChangedEventArgs() { Added = true, Neibour = neibour });
                }
            }
        }


        public void Send()
        {
        }

        public void Receive()
        {
        }

        public void Start()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed != null) changed(this, e);
        }

        private void InitializeComponent()
        {
            this.listenerWorker = new System.ComponentModel.BackgroundWorker();
            this.senderWorker = new System.ComponentModel.BackgroundWorker();
            // 
            // listenerWorker
            // 
            this.listenerWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // senderWorker
            // 
            this.senderWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.senderWorker_DoWork);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void ForwardLog(object sender, OnMessageEventArgs args)
        {
            InvokeOnMessage(args);
        }

        private void senderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
        }
    }
}