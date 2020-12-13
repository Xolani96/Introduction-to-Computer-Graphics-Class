using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HermitArc
{
    public partial class Form1 : Form
    {
        Graphics g;
        PointF p0, p1, t0, t1;
        PointF midPoint1;
        PointF midPoint2;
        Color cControl = Color.Black;
        Color cCurve = Color.Blue;
        Color cTran = Color.Red;
        public Form1()
        {
            InitializeComponent();
            p0 = new PointF(100, 500);
            t0 = new PointF(300, 100);
            p1 = new PointF(500, 400);
            t1 = new PointF(750, 550);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            midPoint1 = new PointF((p0.X + t0.X) / 2, (p0.Y + t0.Y) / 2);
            midPoint2 = new PointF((p1.X + t1.X) / 2, (p1.Y + t1.Y) / 2);
            //Handle this multiplication variable with scrollbar!
            DrawHermiteArc(new Pen(cCurve, 3f), midPoint1, midPoint2,
                Mult(Local(p0, t0), hScrollBar1.Value),
                Mult(Local(p1, t1), hScrollBar1.Value));

            g.FillRectangle(new SolidBrush(cControl), p0.X - 5, p0.Y - 5, 10, 10);
            g.FillRectangle(new SolidBrush(cControl), p1.X - 5, p1.Y - 5, 10, 10);
            g.FillRectangle(new SolidBrush(cTran), midPoint1.X - 5, midPoint1.Y - 5, 10, 10);
            g.FillRectangle(new SolidBrush(cTran), midPoint2.X - 5, midPoint2.Y - 5, 10, 10);
            g.FillRectangle(new SolidBrush(cTran), t0.X - 5, t0.Y - 5, 10, 10);
            g.FillRectangle(new SolidBrush(cTran), t1.X - 5, t1.Y - 5, 10, 10);

            g.DrawLine(new Pen(cControl), p0, midPoint1);
            g.DrawLine(new Pen(cControl), midPoint1,t0);
            g.DrawLine(new Pen(cControl), p1, midPoint2);
            g.DrawLine(new Pen(cControl), midPoint2, t1);
        }
        #region Mouse handling
        int grab = -1;
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsGrab(p0, e.Location)) grab = 0;
            else if (IsGrab(p1, e.Location)) grab = 1;
            else if (IsGrab(t0, e.Location)) grab = 2;
            else if (IsGrab(t1, e.Location)) grab = 3;
            //else if (IsGrab(midPoint1, e.Location)) grab = 4;
            //else if (IsGrab(midPoint2, e.Location)) grab = 5;
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            grab = -1;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grab != -1)
            {
                switch (grab)
                {
                    case 0: p0 = e.Location; 
                            //t0 = NewPoint(e.Location,t0);
                        //Canvas.Refresh();
                        break;
                    case 1: p1 = e.Location; 
                            //t1 = NewPoint(e.Location, t0);
                        //Canvas.Refresh();
                        break;
                    case 2: t0 = e.Location; break;
                    case 3: t1 = e.Location; break;
                    //case 4: midPoint1 = e.Location; break;
                    //case 5: midPoint2 = e.Location; break;
                    default: break;
                }

                Canvas.Invalidate();
            }
        }
        private bool IsGrab(PointF p, PointF m)
        {
            return Math.Abs(p.X - m.X) <= 5 && Math.Abs(p.Y - m.Y) <= 5;
        }
        private PointF NewPoint(PointF p0, PointF t0)
        {
            int dist = (int)Math.Sqrt(Math.Sqrt(t0.X-p0.X) + Math.Sqrt(t0.Y - p0.Y));
            double m = (t0.Y - p0.Y) / (t0.X - p0.X);
            double angle = 1 / Math.Tan(m);
            float newX = (float)(Math.Cos(angle) * dist);
            float newY = (float)(Math.Sin(angle) * dist);
            return new PointF(newX, newY);
        }

        private float H0(float t) { return 2 * t * t * t - 3 * t * t + 1; }
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
            while (t < 1.0)
            {
                t += h;
                d1 = new PointF(H0(t) * p0.X + H1(t) * p1.X + H2(t) * t0.X + H3(t) * t1.X,
                                  H0(t) * p0.Y + H1(t) * p1.Y + H2(t) * t0.Y + H3(t) * t1.Y);
                g.DrawLine(pen, d0, d1);
                d0 = d1;
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Canvas.Refresh();
        }

        private PointF Local(PointF a, PointF b) { return new PointF(b.X - a.X, b.Y - a.Y); }
        private PointF Add(PointF a, PointF b) { return new PointF(b.X + a.X, b.Y + a.Y); }
        private PointF Mult(PointF a, float l) { return new PointF(a.X * l, a.Y * l); }
        #endregion
    }
}
