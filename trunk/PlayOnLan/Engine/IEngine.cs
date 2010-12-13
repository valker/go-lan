using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Engine;

namespace GoLanClient.Engine
{
    public interface INeibour
    {
        string Name { get; }
    }

    public interface IEngine : INotifyPropertyChanged, ILoggable
    {
        IEnumerable<INeibour> Neibours { get; }
        void Start();
        event EventHandler<NeiboursChangedEventArgs> NeiboursChanged;
        string Name { get; set; }
        void Send();
        void Receive();
    }

}
