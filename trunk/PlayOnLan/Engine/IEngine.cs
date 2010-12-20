using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Valker.PlayOnLan.Engine
{
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