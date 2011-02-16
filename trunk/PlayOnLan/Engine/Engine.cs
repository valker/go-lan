using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq.agent;
using agsXMPP.protocol.iq.disco;
using agsXMPP.protocol.iq.roster;
using agsXMPP.Xml.Dom;
using NetworkConnect.Discovery;
using Valker.Api;

namespace Valker.PlayOnLan.Engine
{
    internal class Engine : Component, IEngine
    {
        private BackgroundWorker listenerWorker;
        private BackgroundWorker senderWorker;
        private IEnumerable<INeibour> _neibours = Enumerable.Empty<INeibour>();
        private List<ITransport> _transports = new List<ITransport>();

        public Engine()
        {
            Name = Guid.NewGuid().ToString();
            InitializeComponent();

        }

/*
        private void xmppCon_OnLogin(object sender)
        {
            Debug.WriteLine("Logged");
            _connection.Status = "online";
            _connection.SendMyPresence();
            _disco.DiscoverItems(new Jid(_connection.Server), Cb);
        }
*/

/*
        private void Cb(object sender, IQ iq, object data)
        {
            if (iq.Type == IqType.result)
            {
                Element query = iq.Query;
                if (query != null && query.GetType() == typeof(DiscoItems))
                {
                    DiscoItems items = query as DiscoItems;
                    DiscoItem[] itms = items.GetDiscoItems();

                    foreach (DiscoItem itm in itms)
                    {
                        if (itm.Jid != null)
                            _disco.DiscoverInformation(itm.Jid, new IqCB(Cb), itm);
                    }
                }
            }
        }
*/

/*
        private void xmppCon_OnMessage(object sender, Message msg)
        {
            Debug.WriteLine(msg.Body);
            _connection.Send(new Message(new Jid("valker@MOSDB9VF4J"), msg.Body.ToUpper()));
        }
*/

/*
        private void xmppCon_OnPresence(object sender, Presence pres)
        {
            if(pres.Type == PresenceType.subscribe)
            {
                Debug.WriteLine("request:" + pres.From);
                PresenceManager pm = new PresenceManager(_connection);
                pm.ApproveSubscriptionRequest(pres.From);
            }
        }
*/

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

        public void AddTransport(ITransport transport)
        {
            _transports.Add(transport);
        }

        public void Start()
        {
            foreach (var transport in _transports)
            {
                transport.Start();
            }
        }

        public IEnumerable<ITransport> Transports
        {
            get { return _transports.ToArray(); }
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