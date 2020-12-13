using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParametricCurves
{
    public partial class Form1 : Form
    {
        Graphics g;
        public Form1()
        {
            InitializeComponent();
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            CircleAlgo();
        }
        #region Mouse Handling
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
          
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private bool isFound(PointF p, int dist, PointF mouse)
        {
            return Math.Abs(p.X - mouse.X) <= dist && Math.Abs(p.Y - mouse.Y) <= dist;
        }
        #endregion
        private void CircleAlgo()
        {
            double R = 100;
            double a = 0;
            double b = 2 * Math.PI;
            double t = a;
            float scale = 1f;
            double h = (b - a) / 500.0;

            PointF p0 = new PointF(scale * (float)(R * Math.Cos(t)) + Canvas.Width / 2,
                                   scale * (float)(R * Math.Sin(t)) + Canvas.Height / 2);

            while (t < b)
            {
                t += h;
                PointF p1 = new PointF(scale * (float)(R * Math.Cos(t)) + Canvas.Width / 2,
                                       scale * (float)(R * Math.Sin(t)) + Canvas.Height / 2);
                g.DrawLine(Pens.Black, p0, p1);
                p0 = p1;
            }
        }
    }
}
