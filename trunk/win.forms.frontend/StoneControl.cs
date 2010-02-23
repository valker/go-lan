using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace win.forms.frontend
{
    public partial class StoneControl : UserControl
    {
        public StoneControl()
        {
            InitializeComponent();
        }
        bool _isBlack;
        public bool IsBlack { get { return _isBlack; } set {
            if (_isBlack == value) return;
            _isBlack = value;
            Update();
            Invalidate();
        }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var size = Math.Min(Width, Height);
            var size2 = size - 1;
            var graphics = e.Graphics;
            switch (IsBlack)
            {
                case true:
                    graphics.FillEllipse(Brushes.Black, 0, 0, size2, size2);
                    break;
                case false:
                    graphics.FillEllipse(Brushes.White, 0, 0, size2, size2);
                    graphics.DrawEllipse(Pens.Black, 0, 0, size2, size2);
                    break;
            }

            base.OnPaint(e);
        }
    }
}
