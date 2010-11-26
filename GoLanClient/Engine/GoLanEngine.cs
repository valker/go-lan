using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GoLanClient.Engine
{
    internal class GoLanEngine : Component, IEngine
    {
        private BackgroundWorker backgroundWorker1;
        private IEnumerable<INeibour> _neibours = Enumerable.Empty<INeibour>();

        public GoLanEngine()
        {
            Rules = new List<IRules>();
            InitializeComponent();
        }

        public event EventHandler<NeiboursChangedEventArgs> NeiboursChanged;

        private void InvokeNeiboursChanged(NeiboursChangedEventArgs e)
        {
            EventHandler<NeiboursChangedEventArgs> handler = NeiboursChanged;
            if (handler != null) handler(this, e);
        }

        public IEnumerable<INeibour> Neibours
        {
            get { return _neibours; }
            set
            {
                var removed = _neibours.Except(value);
                var added = value.Except(_neibours);
                _neibours = value.ToArray();
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

        public ICollection<IRules> Rules
        {
            get; set;
        }

        public void Start()
        {
            backgroundWorker1.RunWorkerAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed != null) changed(this, e);
        }

        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SimulateMonitoring);

        }

        private void SimulateMonitoring(object sender, DoWorkEventArgs e)
        {
            var me = (BackgroundWorker) sender;
            int i = 1;
            while (!me.CancellationPending)
            {
                System.Threading.Thread.Sleep(2000);
                Neibours = Neibours.Concat(Enumerable.Repeat((INeibour) new Neibour(){Name = i.ToString()}, 1)).ToArray();
                ++i;

                if (i > 4)
                {
                    var position = new Random().Next() % Neibours.Count();
                    if (position < Neibours.Count())
                    {
                        IEnumerable<INeibour> x;
                        
                        if (position > 0)
                        {
                            x = Neibours.Take(position).ToArray();
                        } 
                        else
                        {
                            x = Enumerable.Empty<INeibour>();
                        }

                        IEnumerable<INeibour> enumerable = Neibours.Skip(position + 1).ToArray();
                        var neibours = x.Concat(enumerable).ToArray();
                        Neibours = neibours;
                    }
                }
            }
        }
    }
}