using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GoLanClient.Engine
{
    public interface IEngine : INotifyPropertyChanged
    {
        IEnumerable<INeibour> Neibours { get; }
        ICollection<IRules> Rules { get; }
        void Start();
        event EventHandler<NeiboursChangedEventArgs> NeiboursChanged;
    }
}
