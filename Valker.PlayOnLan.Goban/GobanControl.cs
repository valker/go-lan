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
            Hoshi = Enumerable.Empty<Point>();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            N = DefaultN;
            InitializeComponent();
        }

        public double K { get; private set; }
        public double LeftMargin { get; private set; }
        public double TopMargin { get; private set; }

        [Browsable(true)]
        [DefaultValue(DefaultN)]
        public int N
        {
            get { return _n; }
            set
            {
                if (value < 3 || value > 100) throw new ArgumentOutOfRangeException();

                if (_n == value)
                {
                    return;
                }

                _n = value;
                Field = new Stone[_n,_n];
                InvokePropertyChanged(new PropertyChangedEventArgs("N"));
                Hoshi = GetHoshi(_n);
                Recalculate();
                Invalidate();
            }
        }

        protected int OffsetX { get; set; }

        protected int OffsetY { get; set; }

        protected IEnumerable<Point> Hoshi { get; set; }

        internal Stone[,] Field
        {
            get { return _field; }
            set { _field = value; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public event EventHandler<MouseEventArgs> ClickedOnBoard;

        private void InvokeClickedOnBoard(MouseEventArgs e)
        {
            EventHandler<MouseEventArgs> handler = ClickedOnBoard;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // size in millimeters

        public void SetStone(int x, int y, Stone stone, bool invalidate)
        {
            if (x < 0 || y < 0 || x >= _n || y >= _n)
                throw new ArgumentOutOfRangeException();

            Field[x, y] = stone;
            if (invalidate)
            {
                Rectangle rc = GetRect(x, y);
                rc.Width++;
                rc.Height++;
                Invalidate(rc);
            }
        }

        public void SetStone(Point point, Stone stone)
        {
            SetStone(point.X, point.Y, stone, true);
        }

        public Stone GetStone(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _n || y >= _n)
                throw new ArgumentOutOfRangeException();

            return Field[x, y];
        }

        public Stone GetStone(Point point)
        {
            return GetStone(point.X, point.Y);
        }

        private void Recalculate()
        {
            LeftMargin = (BoardWidth - ((N - 1.0) * CellWidth)) / 2.0;
            TopMargin = (BoardHeight - ((N - 1.0) * CellHeight)) / 2.0;

            int min = Math.Min(Size.Height - Offset * 2, Size.Width - Offset * 2);
            K = min / BoardHeight;

            double boardWidthInPixels = K * BoardWidth;
            double boardHeightInPixels = K * BoardHeight;
            OffsetX = (int) ((Size.Width - boardWidthInPixels) / 2);
            OffsetY = (int) ((Size.Height - boardHeightInPixels) / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var width = (int) (BoardWidth * K);
            var height = (int) (BoardHeight * K);
            // TODO:
            e.Graphics.DrawImage(Resources.bamboo_resized, OffsetX, OffsetY, width, height);
            DrawGrid(e);
            DrawHoshi(e, Hoshi);
            DrawStones(e);
        }

        private void DrawStones(PaintEventArgs args)
        {
            int n = N;
            for (int x = 0; x < n; ++x)
            {
                for (int y = 0; y < n; ++y)
                {
                    Stone stone = Field[x, y];
                    if (stone != Stone.None)
                    {
                        Color color = stone == Stone.Black ? Color.Black : Color.White;

                        Rectangle rect = GetRect(x, y);

                        args.Graphics.FillEllipse(new SolidBrush(color), rect);
                        args.Graphics.DrawEllipse(Pens.Black, rect);
                    }
                }
            }
        }

        private Rectangle GetRect(int x, int y)
        {
            int rectx = OffsetX + (int) ((LeftMargin + x * CellWidth - StoneDiameter / 2.0) * K);
            int recty = OffsetY + (int) ((TopMargin + y * CellHeight - CellHeight / 2.0) * K);

            var rectw = (int) (CellWidth * K);
            var recth = (int) (CellHeight * K);
            return new Rectangle(rectx, recty, rectw, recth);
        }

        private void DrawHoshi(PaintEventArgs args, IEnumerable<Point> points)
        {
            foreach (Point point in points)
            {
                int x = OffsetX + (int) ((LeftMargin + point.X * CellWidth - HoshiDiameter / 2.0) * K);
                int y = OffsetY + (int) ((TopMargin + point.Y * CellHeight - HoshiDiameter / 2.0) * K);
                var width = (int) (HoshiDiameter * K);
                var height = (int) (HoshiDiameter * K);
                args.Graphics.FillEllipse(Brushes.Black, x, y, width, height);
            }
        }

        private static IEnumerable<Point> GetHoshi(int n)
        {
            switch (n)
            {
            case 9:
                return new[] {new Point(2, 2), new Point(2, 6), new Point(6, 2), new Point(6, 6), new Point(4, 4)};

            case 13:
                return new[]
                       {
                           new Point(3, 3), new Point(3, 6), new Point(3, 9), new Point(6, 3), new Point(6, 6),
                           new Point(6, 9), new Point(9, 3), new Point(9, 6), new Point(9, 9)
                       };
            case 19:
                return new[]
                       {
                           new Point(3, 3), new Point(3, 9), new Point(3, 15), new Point(9, 3), new Point(9, 9),
                           new Point(9, 15), new Point(15, 3), new Point(15, 9), new Point(15, 15)
                       };

            default:
                return new[] {new Point((n - 1) / 2, (n - 1) / 2)};
            }
        }

        private void DrawGrid(PaintEventArgs e)
        {
            for (int i = 0; i < N; ++i)
            {
                int x = OffsetX + (int) ((LeftMargin + CellWidth * i) * K);
                int y1 = OffsetY + (int) (TopMargin * K);
                int y2 = OffsetY + (int) ((TopMargin + (N - 1) * CellHeight) * K);
                e.Graphics.DrawLine(Pens.Black, x, y1, x, y2);

                int y = OffsetY + (int) ((TopMargin + CellHeight * i) * K);
                int x1 = OffsetX + (int) (LeftMargin * K);
                int x2 = OffsetX + (int) ((LeftMargin + (N - 1) * CellWidth) * K);
                e.Graphics.DrawLine(Pens.Black, x1, y, x2, y);
            }
        }

        private void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed != null)
            {
                changed(this, e);
            }
        }

        private void UserControl1_Resize(object sender, EventArgs e)
        {
            Recalculate();
        }

        private void Goban_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X - OffsetX;
            int y = e.Y - OffsetY;

            double xd = x / K;
            double yd = y / K;

            double xm = xd - LeftMargin;
            double ym = yd - TopMargin;

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
            if (xr > (N - 1) || yr > (N - 1))
            {
                return;
            }

            InvokeClickedOnBoard(new MouseEventArgs(e.Button, e.Clicks, xr, yr, 0));
        }
    }
}