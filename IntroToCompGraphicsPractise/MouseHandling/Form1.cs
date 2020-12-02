using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseHandling
{
    public partial class Form1 : Form
    {
        Brush brush = new SolidBrush(Color.Red);
        PointF p1;
        Graphics g;
        int size = 200;
        float speed = 3;
        bool gotcha = false;
        int dirX = 1, dirY = 1;
        float dx, dy;
        public Form1()
        {
            InitializeComponent();
            p1 = new PointF(100, 400);
            timer.Start();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.FillRectangle(brush, p1.X, p1.Y, size, size);
        }

        #region Mouse Handling
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            speed += 0.5f;
            size -= 10;
            gotcha = false;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (gotcha)
            {
                p1 = new PointF(e.X - dx, e.Y - dy);

                Canvas.Refresh();
            }
        }

        private void timer_Tick_1(object sender, EventArgs e)
        {
            if (!gotcha)
            {
                p1 = new PointF(p1.X + dirX * speed, p1.Y + dirY * speed);

                if (p1.Y + size >= Canvas.Height)
                {
                    p1.Y = Canvas.Height - size - 1;
                    dirY *= -1;
                }
                if (p1.Y <= 0)
                {
                    p1.Y = 1;
                    dirY *= -1;
                }
                //HW.: Vertical check
                if (p1.X + size >= Canvas.Width)
                {
                    p1.X = Canvas.Width - size - 1;
                    dirX *= -1;
                }
                if (p1.X <= 0)
                {
                    p1.X = 1;
                    dirX *= -1;
                }

                Canvas.Refresh();
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if(p1.X<= e.X && e.X <= p1.X + size && p1.Y <= e.Y && e.Y <= p1.Y + size)
            {
                if (size == 150)
                {
                    timer.Stop();
                    MessageBox.Show("You win!");
                }

                dx = e.X - p1.X;
                dy = e.Y - p1.Y;
                gotcha = true;
            }
        }
        #endregion
        
    }
}
