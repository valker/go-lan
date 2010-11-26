using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GoLanClient.Engine
{
    public interface INeibour
    {
        string Name { get; }
    }

    public interface IEngine : INotifyPropertyChanged
    {
        IEnumerable<INeibour> Neibours { get; }
        void Start();
        event EventHandler<NeiboursChangedEventArgs> NeiboursChanged;
    }
}
