using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GoLanClient.Engine;
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

        public GoLanEngine(bool isServer)
        {
            _isServer = isServer;
            Name = Guid.NewGuid().ToString();
            InitializeComponent();
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
            senderWorker.RunWorkerAsync();
        }

        public void Receive()
        {
            listenerWorker.RunWorkerAsync();
        }

        private Discoverer _discoverer;
        private bool _isServer;

        public void Start()
        {
            _discoverer = new Discoverer(Name, _isServer);
            _discoverer.OnMessage += ForwardLog;
            _discoverer.StartListener(_isServer);
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
            _listener = new Listener(Name);
            _listener.OnMessage += ForwardLog;
            _listener.Run();
        }

        private void ForwardLog(object sender, OnMessageEventArgs args)
        {
            InvokeOnMessage(args);
        }

        private void senderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _sender = new Sender(Name);
            _sender.OnMessage += ForwardLog;
            _sender.Run();
        }
    }

    public class NeiboursChangedEventArgs : EventArgs
    {
        public bool Added { get; set; }
        public INeibour Neibour { get; set; }
    }

    internal class Neibour : INeibour
    {
        public string Name
        {
            get; set;
        }
    }
}