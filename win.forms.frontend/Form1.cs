using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using go_engine;
using go_engine.Data;
using Point=Microsoft.Xna.Framework.Point;

namespace win.forms.frontend
{
    public partial class Form1 : Form
    {
        private int _CellSize;
        public GameManager Manager { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Manager.CurrentPositionChanged += ManagerOnCurrentPositionChanged;
            UpdateGobanImage();
        }

        private void ManagerOnCurrentPositionChanged(object sender, EventArgs args)
        {
            UpdateGobanImage();
        }

        private void UpdateGobanImage()
        {
            var boardInfo = Manager.RootPosition.Field.Size;

            _CellSize = Math.Min(goban.Width, goban.Height) / boardInfo;

            int cellSize2 = _CellSize / 2;
            var cells = boardInfo;
            var size = cells * _CellSize;
            var bitmap = new Bitmap(size, size);
            var graphics = Graphics.FromImage(bitmap);

            for (var row = 0; row < cells; row++)
            {
                graphics.DrawLine(SystemPens.ControlDark, cellSize2, cellSize2 + row * _CellSize, cellSize2 + (cells - 1) * _CellSize, cellSize2 + row * _CellSize);
                graphics.DrawLine(SystemPens.ControlDark, cellSize2 + row * _CellSize, cellSize2, cellSize2 + row * _CellSize, cellSize2 + (cells - 1) * _CellSize);
            }


            for (var row = 0; row < cells; row++)
            {
                for (var col = 0; col < cells; col++)
                {
                    var point = new Point(col, row);
                    switch (Manager.CurrentPosition.Field.GetAt(point))
                    {
                        case MokuState.Black:
                            graphics.FillEllipse(Brushes.Black, col * _CellSize + 1, row * _CellSize + 1, _CellSize - 2, _CellSize - 2);
                            break;
                        case MokuState.White:
                            graphics.FillEllipse(Brushes.White, col * _CellSize + 1, row * _CellSize + 1, _CellSize - 2, _CellSize - 2);
                            graphics.DrawEllipse(Pens.Black, col * _CellSize + 1, row * _CellSize + 1, _CellSize - 2, _CellSize - 2);
                            break;
                    }
                }
            }
            goban.Image = bitmap;
        }

        private void goban_MouseClick(object sender, MouseEventArgs e)
        {

            int x = e.X / _CellSize;
            int y = e.Y / _CellSize;
            Debug.WriteLine(string.Format("new Point({0},{1}),", x, y));

            try
            {
                Manager.Move(Manager.CurrentPosition, new Point(x, y));

            }
            catch (GoException ex)
            {
                MessageBox.Show(ex.Reason.ToString());
            }
        }
    }
}
