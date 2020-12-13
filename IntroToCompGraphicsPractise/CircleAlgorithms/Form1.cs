using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircleAlgorithms
{
    public partial class Form1 : Form
    {
        Graphics g;
        Point C, P;
        int found = -1;
        int distance = 5;
        public Form1()
        {
            InitializeComponent(); 
            C = new Point(Canvas.Width / 2, Canvas.Height / 2);
            P = new Point(300, 400);
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            Circle(Color.Black, C, (int)Math.Sqrt((C.X - P.X) * (C.X - P.X) + (C.Y - P.Y) * (C.Y - P.Y)));

            g.DrawRectangle(Pens.Black, C.X - 5, C.Y - 5, 10, 10);
            g.DrawRectangle(Pens.Black, P.X - 5, P.Y - 5, 10, 10);
            g.DrawLine(Pens.Black, C, P);
        }
        #region Mouse Handling
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsFound(C, distance, e.Location)) found = 0;
            else if(IsFound(P,distance,e.Location)) found = 1;
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            found = -1;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (found != -1)
            {
                switch (found)
                {
                    case 0:
                        P = e.Location;
                        break;
                    case 1:
                        C = e.Location;
                        P = e.Location;
                        break;
                    default:
                        break;
                }
                Canvas.Refresh();
            }
        }

        private bool IsFound(PointF p, int dist, PointF mouse)
        {
            return Math.Abs(p.X - mouse.X) <= dist && Math.Abs(p.Y - mouse.Y)<= dist;
        }
        #endregion

        private void CirclePoint(Color c, Point p, Point tran)
        {
            Pen pen = new Pen(c);
            g.DrawRectangle(pen, p.X + tran.X, p.Y + tran.Y, 0.5f, 0.5f);
            g.DrawRectangle(pen, -p.X + tran.X, -p.Y + tran.Y, 0.5f, 0.5f);
            g.DrawRectangle(pen, -p.X + tran.X, p.Y + tran.Y, 0.5f, 0.5f);
            g.DrawRectangle(pen, p.X + tran.X, -p.Y + tran.Y, 0.5f, 0.5f);
            g.DrawRectangle(pen, p.Y + tran.X, p.X + tran.Y, 0.5f, 0.5f);
            g.DrawRectangle(pen, -p.Y + tran.X, -p.X + tran.Y, 0.5f, 0.5f);
            g.DrawRectangle(pen, -p.Y + tran.X, p.X + tran.Y, 0.5f, 0.5f);
            g.DrawRectangle(pen, p.Y + tran.X, -p.X + tran.Y, 0.5f, 0.5f);
        }
        private void Circle(Color c, Point C, int R)
        {
            int x = 0;
            int y = R;
            int h = 1 - R;
            CirclePoint(c, new Point(x, y), C);
            while (y > x)
            {
                if (h < 0)
                    h += 2 * x + 3;
                else
                {
                    h += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                CirclePoint(c, new Point(x, y), C);
            }
        }

        //private Point Newp(Point center, Point P)
        //{
        //    //int distC2P = (int)Math.Sqrt((center.X - P.X) * (center.X - P.X) + (center.Y - P.Y) * (center.Y - P.Y));
        //    //double ov = Math.Sin(P.Y / distC2P);
        //    //double angle = 1 /ov ;
        //    //return new Point((int)(distC2P * Math.Cos(angle)), (int)(distC2P * Math.Sin(angle)));

            
        //}
    }
}
