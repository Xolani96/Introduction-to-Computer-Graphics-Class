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
        }
        #region Mouse Handling
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsFound(p0, distance, e.Location)) found = 0;
            else if(IsFound(p1,distance,e.Location)) found = 1;
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
                        p0 = e.Location;
                        break;
                    case 1:
                        p1 = e.Location;
                        break;
                    default:
                        break;
                }
            }
        }

        private bool IsFound(PointF p, int dist, PointF mouse)
        {
            return Math.Abs(p.X - mouse.X) <= dist && Math.Abs(p.Y - mouse.Y)<= dist;
        }
        #endregion
    }
}
