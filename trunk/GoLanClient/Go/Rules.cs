using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GoLanClient.Engine;
using MyGoban;

namespace GoLanClient.Go
{
    class Rules : IRules
    {
        public override string ToString()
        {
            return "Go";
        }

        public Control GetControl()
        {
            return new MyGoban.Goban();
        }
    }
}
