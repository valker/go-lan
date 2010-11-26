using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyGoban;

namespace GoLanClient.Engine
{
    public interface IRules
    {
        Control GetControl();
    }
}
