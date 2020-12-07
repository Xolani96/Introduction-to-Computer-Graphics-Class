using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineAlgorithms
{
    public partial class Form1 : Form
    {
        Graphics g;
        PointF p0, p1;
        int found = -1;
        int distance = 5;
        public Form1()
        {
            InitializeComponent();
            p0 = new PointF(100, 100);
            p1 = new PointF(500, 300);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            //g.DrawLine(new Pen(Color.Brown), p0, p1);
            
            //DDA(new Pen(Color.Brown), p0.X, p0.Y, p1.X, p1.Y);
            //Midpoint45(new Pen(Color.Brown), p0.X, p0.Y, p1.X, p1.Y);
            Midpoint(new Pen(Color.Brown), p0.X, p0.Y, p1.X, p1.Y);
            g.FillRectangle(new SolidBrush(Color.Black), p0.X - distance, p0.Y - distance, 2 * distance, 2 * distance);
            g.FillRectangle(new SolidBrush(Color.Red), p1.X - distance, p1.Y - distance, 2 * distance, 2 * distance);
        }
        #region Mouse Handling
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (isFound(p0, distance, e.Location)) found = 0;
            else if (isFound(p1, distance, e.Location)) found = 1;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(found != -1)
            {
                switch (found)
                {
                    case 0: p0 = e.Location;
                        break;
                    case 1: p1 = e.Location;
                        break;
                    default:
                        break;
                }
                Canvas.Refresh();
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            found = -1;
        }
        private bool isFound(PointF p, int dist, PointF mouse)
        {
            return Math.Abs(p.X - mouse.X) <= dist && Math.Abs(p.Y - mouse.Y) <= dist;
        }
        #endregion
        #region Line Algorithms
        private void DDA(Pen pen , float x0, float y0, float x1,float y1)
        {
            float Dx, Dy, Length, X_inc, Y_inc, X, Y;
            int i;

            Dx = x1 - x0;
            Dy = y1 - y0;

            Length = Math.Abs(Dx);
            if (Length < Math.Abs(Dy))
                Length = Math.Abs(Dy);

            X_inc = Dx / Length;
            Y_inc = Dy / Length;

            X = x0;
            Y = y0;

            for (i = 0; i < Length; i++)
            {
                X = X + X_inc;
                Y = Y + Y_inc;
                g.DrawLine(pen, x0,y0, x1, y1);
            }
        }
        private void Midpoint45(Pen pen, float x0, float y0, float x1, float y1)
        {
            float D, Dx, Dy,  X, Y;

            Dx = x1 - x0;
            Dy = y1 - y0;
            D = 2 * (Dy - Dx);

            X = x0;
            Y = y0;

            for (int i = 0; i < Dx; i++)
            {
                g.DrawLine(pen, x0, y0, x1, y1);
                if (D > 0)
                {
                    Y = Y + 1;
                    D = D + 2 * (Dy - Dx);
                }
                else
                    D = D + 2 * Dy;
            }
            X = X + 1;
        }
        private void Midpoint(Pen pen, float x0, float y0, float x1, float y1)
        {
            int D, DY, DX, R, SX, SY, X, Y;
            bool T;

            DX = (int)Math.Abs(x1 - x0);
            DY = (int)Math.Abs(y1-y0);
            SX = Math.Sign(x1 - x0);
            SY = Math.Sign(y1 - y0);
            if (DX < DY)
            {
                R = DX;
                DX = DY;
                DY = R;
                T = true;
            }
            else
                T = false;

            D = 2 * (DY - DX);
            X = (int)x0;
            Y = (int)y0;

            g.DrawLine(pen, x0, y0, x1, y1);
            while ((X != x1) && (Y != y1))
            {
                if (D > 0)
                {
                    if (T is true)
                        X = X + SX;
                    else Y = Y + SY;

                    D = D + 2 * DY;
                }
                if (T is true)
                    Y = Y + SY;
                else X = X + SX;

                g.DrawLine(pen, x0, y0, x1, y1);
            }
        }
        #endregion
    }
}
