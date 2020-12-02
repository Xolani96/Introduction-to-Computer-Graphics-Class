using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicsAndTools
{
    public partial class Form1 : Form
    {
        Graphics g;
        Point p1, p2, center;
        Point[] points;
        public Form1()
        {
            InitializeComponent();
            Point p1 = new Point(100,200);
            Point p2 = new Point(400, 500);
            points = new Point[] { new Point(100,300), new Point(300, 700), new Point(700, 500), 
                                    new Point(500, 145), new Point(145, 400), };
            center = new Point(Canvas.Width/2, Canvas.Height/2); 
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            Pen black = new Pen(Color.Black);
            //Line
            g.DrawLine(black, 100, 200, 400, 500);
            g.DrawLines(black, points);
            g.DrawLine(Pens.Black, 0, center.Y, Canvas.Width, center.Y);
            g.DrawLine(Pens.Green, new PointF(0, 0), center);

            //Rectangle and Ellipse
            g.FillRectangle(new SolidBrush(Color.Yellow), 100, 100, 200, 300);
            g.DrawRectangle(black, 100, 100, 200, 300);
            g.FillEllipse(Brushes.Red, 100, 100, 200, 300);
            g.DrawEllipse(Pens.Magenta, 100, 100, 200, 300);

            g.DrawRectangle(Pens.Brown, 100, 100, 0.5f, 0.5f);
            float r = 15;
            g.DrawEllipse(Pens.Red, 600 - r, 100 - r, 2 * r, 2 * r);

            for (int i = 0; i < 256; i++)
                for (int j = 0; j < 256; j++)
                    g.DrawRectangle(new Pen(Color.FromArgb(i, j, j)),
                        center.X + i, center.Y + j, 0.5f, 0.5f);

        }
        #region Mouse handling
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
        #endregion
    }
}
