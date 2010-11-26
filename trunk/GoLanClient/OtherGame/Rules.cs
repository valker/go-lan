using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GoLanClient.Engine;

namespace GoLanClient.OtherGame
{
    class Rules : IRules
    {
        public Control GetControl()
        {
            return new Label(){Text = "No controls"};
        }

        public override string ToString()
        {
            return "Other Game";
        }
    }
}
