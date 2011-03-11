using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Valker.PlayOnLan.Goban.Properties;


namespace Valker.PlayOnLan.Goban
{
    public partial class GobanControl : UserControl, INotifyPropertyChanged
    {
        private const double BoardHeight = 454.5;
        private const double BoardWidth = 424.2;
        private const double CellHeight = 24.7;
        private const double CellWidth = 23.0;
        private const int DefaultN = 19;
        private const double HoshiDiameter = 4.0;
        private const int Offset = 10;
        private const double StoneDiameter = 22.5;

        private Stone[,] _field = new Stone[19,19];
        private int _n;

        public GobanControl()
        {
            this.Hoshi = Enumerable.Empty<Point>();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.N = DefaultN;
            this.InitializeComponent();
        }

        public double K { get; private set; }
        public double LeftMargin { get; private set; }
        public double TopMargin { get; private set; }

        [Browsable(true)]
        [DefaultValue(DefaultN)]
        public int N
        {
            get { return this._n; }
            set
            {
                if (this._n == value)
                {
                    return;
                }
                this._n = value;
                this.Field = new Stone[this._n,this._n];
                this.InvokePropertyChanged(new PropertyChangedEventArgs("N"));
                this.Hoshi = GetHoshi(this._n);
                this.Recalculate();
                this.Invalidate();
            }
        }

        protected int OffsetX { get; set; }

        protected int OffsetY { get; set; }

        protected IEnumerable<Point> Hoshi { get; set; }

        internal Stone[,] Field
        {
            get { return this._field; }
            set { this._field = value; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public event EventHandler<MouseEventArgs> ClickedOnBoard;

        private void InvokeClickedOnBoard(MouseEventArgs e)
        {
            EventHandler<MouseEventArgs> handler = this.ClickedOnBoard;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // size in millimeters

        public void SetStone(int x, int y, Stone stone, bool invalidate)
        {
            this.Field[x, y] = stone;
            if (invalidate)
            {
                Rectangle rc = this.GetRect(x, y);
                rc.Width++;
                rc.Height++;
                Invalidate(rc);
            }
        }

        public void SetStone(Point point, Stone stone)
        {
            this.SetStone(point.X, point.Y, stone, true);
        }

        public Stone GetStone(int x, int y)
        {
            return this.Field[x, y];
        }

        public Stone GetStone(Point point)
        {
            return this.GetStone(point.X, point.Y);
        }

        private void Recalculate()
        {
            this.LeftMargin = (BoardWidth - ((this.N - 1.0) * CellWidth)) / 2.0;
            this.TopMargin = (BoardHeight - ((this.N - 1.0) * CellHeight)) / 2.0;

            int min = Math.Min(this.Size.Height - Offset * 2, this.Size.Width - Offset * 2);
            this.K = min / BoardHeight;

            double boardWidthInPixels = this.K * BoardWidth;
            double boardHeightInPixels = this.K * BoardHeight;
            this.OffsetX = (int) ((this.Size.Width - boardWidthInPixels) / 2);
            this.OffsetY = (int) ((this.Size.Height - boardHeightInPixels) / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var width = (int) (BoardWidth * this.K);
            var height = (int) (BoardHeight * this.K);
            // TODO:
            e.Graphics.DrawImage(Resources.bamboo_resized, this.OffsetX, this.OffsetY, width, height);
            this.DrawGrid(e);
            this.DrawHoshi(e, this.Hoshi);
            this.DrawStones(e);
        }

        private void DrawStones(PaintEventArgs args)
        {
            int n = this.N;
            for (int x = 0; x < n; ++x)
            {
                for (int y = 0; y < n; ++y)
                {
                    Stone stone = this.Field[x, y];
                    if (stone != Stone.None)
                    {
                        Color color = stone == Stone.Black ? Color.Black : Color.White;

                        Rectangle rect = this.GetRect(x, y);

                        args.Graphics.FillEllipse(new SolidBrush(color), rect);
                        args.Graphics.DrawEllipse(Pens.Black, rect);
                    }
                }
            }
        }

        private Rectangle GetRect(int x, int y)
        {
            int rectx = this.OffsetX + (int) ((this.LeftMargin + x * CellWidth - StoneDiameter / 2.0) * this.K);
            int recty = this.OffsetY + (int) ((this.TopMargin + y * CellHeight - CellHeight / 2.0) * this.K);

            var rectw = (int) (CellWidth * this.K);
            var recth = (int) (CellHeight * this.K);
            return new Rectangle(rectx, recty, rectw, recth);
        }

        private void DrawHoshi(PaintEventArgs args, IEnumerable<Point> points)
        {
            foreach (Point point in points)
            {
                int x = this.OffsetX + (int) ((this.LeftMargin + point.X * CellWidth - HoshiDiameter / 2.0) * this.K);
                int y = this.OffsetY + (int) ((this.TopMargin + point.Y * CellHeight - HoshiDiameter / 2.0) * this.K);
                var width = (int) (HoshiDiameter * this.K);
                var height = (int) (HoshiDiameter * this.K);
                args.Graphics.FillEllipse(Brushes.Black, x, y, width, height);
            }
        }

        private static IEnumerable<Point> GetHoshi(int n)
        {
            switch (n)
            {
            case 9:
                return new[] {new Point(2, 2), new Point(2, 6), new Point(6, 2), new Point(6, 6), new Point(4, 4),};

            case 13:
                return new[]
                       {
                           new Point(3, 3), new Point(3, 6), new Point(3, 9), new Point(6, 3), new Point(6, 6),
                           new Point(6, 9), new Point(9, 3), new Point(9, 6), new Point(9, 9),
                       };
            case 19:
                return new[]
                       {
                           new Point(3, 3), new Point(3, 9), new Point(3, 15), new Point(9, 3), new Point(9, 9),
                           new Point(9, 15), new Point(15, 3), new Point(15, 9), new Point(15, 15),
                       };

            default:
                return new[] {new Point((n - 1) / 2, (n - 1) / 2),};
            }
        }

        private void DrawGrid(PaintEventArgs e)
        {
            for (int i = 0; i < this.N; ++i)
            {
                int x = this.OffsetX + (int) ((this.LeftMargin + CellWidth * i) * this.K);
                int y1 = this.OffsetY + (int) (this.TopMargin * this.K);
                int y2 = this.OffsetY + (int) ((this.TopMargin + (this.N - 1) * CellHeight) * this.K);
                e.Graphics.DrawLine(Pens.Black, x, y1, x, y2);

                int y = this.OffsetY + (int) ((this.TopMargin + CellHeight * i) * this.K);
                int x1 = this.OffsetX + (int) (this.LeftMargin * this.K);
                int x2 = this.OffsetX + (int) ((this.LeftMargin + (this.N - 1) * CellWidth) * this.K);
                e.Graphics.DrawLine(Pens.Black, x1, y, x2, y);
            }
        }

        private void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler changed = this.PropertyChanged;
            if (changed != null)
            {
                changed(this, e);
            }
        }

        private void UserControl1_Resize(object sender, EventArgs e)
        {
            this.Recalculate();
        }

        private void Goban_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X - this.OffsetX;
            int y = e.Y - this.OffsetY;

            double xd = x / this.K;
            double yd = y / this.K;

            double xm = xd - this.LeftMargin;
            double ym = yd - this.TopMargin;

            double xc = xm + CellWidth / 2;
            double yc = ym + CellHeight / 2;

#if false
            Debug.WriteLine("xd,yd:" + xd + " " + yd);
            Debug.WriteLine("xm,ym:" + xm + " " + ym);
            Debug.WriteLine("xc,yc:" + xc + " " + yc);
#endif
            if (xc <= 0.0 || yc <= 0.0)
            {
                return;
            }
            var xr = (int) (xc / CellWidth);
            var yr = (int) (yc / CellHeight);
            if (xr > (this.N - 1) || yr > (this.N - 1))
            {
                return;
            }

            this.InvokeClickedOnBoard(new MouseEventArgs(e.Button, e.Clicks, xr, yr, 0));
        }
    }
}