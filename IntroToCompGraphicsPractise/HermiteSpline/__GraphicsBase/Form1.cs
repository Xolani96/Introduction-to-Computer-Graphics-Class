using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace __GraphicsBase
{
    public partial class Form1 : Form
    {
        Graphics g;

        List<PointF> P = new List<PointF>();

        Color cControl = Color.LightGray;
        Color cCurve = Color.Blue;

        public Form1()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //Handle this multiplication variable with scrollbar!
            float mult = 1.5f;
            for (int i = 0; i < P.Count - 3; i += 2)
                DrawHermiteArc(new Pen(cCurve, 3f), P[i], P[i + 2],
                    Mult(Local(P[i], P[i + 1]), hScrollBar1.Value),
                    Mult(Local(P[i + 2], P[i + 3]), hScrollBar1.Value));

            for (int i = 0; i < P.Count; i += 2)
                g.FillRectangle(new SolidBrush(cControl), P[i].X - 5, P[i].Y - 5, 10, 10);
            ////Draw the opposit of theese lines (imlement the handling of them with mouse)  
            for (int i = 0; i < P.Count - 1; i += 2)
                g.DrawLine(new Pen(cControl), P[i], P[i + 1]);                  
        }

        #region Mouse handling
        int grab = -1;
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < P.Count; i++)
            {
                if (IsGrab(P[i], e.Location))
                    grab = i;
            }

            if (grab == -1)
            {
                P.Add(e.Location);
                P.Add(e.Location);
                grab = P.Count - 1;
                canvas.Invalidate();
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            //HW.: If I modify a point, than translate the tangent also!
            //So if we modify the location of a point than  the direction
            //of a tangent vector has to keep it's direction
            if (grab != -1)
            {
                P[grab] = e.Location;

                canvas.Invalidate();
            }
        }
        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            grab = -1;
        }
        private void canvas_MouseWheel(object sender, MouseEventArgs e)
        {

        }

        private bool IsGrab(PointF p, PointF m)
        {
            return Math.Abs(p.X - m.X) <= 5 && Math.Abs(p.Y - m.Y) <= 5;
        }
        #endregion

        private float H0 (float t) { return 2 * t * t * t - 3 * t * t + 1; }
        private float H1(float t) { return -2 * t * t * t + 3 * t * t; }
        private float H2(float t) { return t * t * t - 2 * t * t + t; }
        private float H3(float t) { return t * t * t - t * t; }

        private void DrawHermiteArc(Pen pen, PointF p0, PointF p1, PointF t0, PointF t1)
        {
            float t = 0;
            float h = 1.0f / 500.0f;
            PointF d0, d1;
            d0 = new PointF(H0(t) * p0.X + H1(t) * p1.X + H2(t) * t0.X + H3(t) * t1.X,
                            H0(t) * p0.Y + H1(t) * p1.Y + H2(t) * t0.Y + H3(t) * t1.Y);
            while(t < 1.0)
            {
                t += h;
                d1 = new PointF(H0(t) * p0.X + H1(t) * p1.X + H2(t) * t0.X + H3(t) * t1.X,
                                  H0(t) * p0.Y + H1(t) * p1.Y + H2(t) * t0.Y + H3(t) * t1.Y);
                g.DrawLine(pen, d0, d1);
                d0 = d1;
            }
        }

        private PointF Local(PointF a, PointF b) { return new PointF(b.X - a.X, b.Y - a.Y); }
        private PointF Add(PointF a, PointF b) { return new PointF(b.X + a.X, b.Y + a.Y); }
        private PointF Mult(PointF a, float l) { return new PointF(a.X * l, a.Y * l); }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            canvas.Refresh();
        }
    }
}
