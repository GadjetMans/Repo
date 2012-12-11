using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Repo
{
    public partial class MainScreen : Form
    {
        public abstract class Shape
        {
            public abstract void DrawWith(Graphics g);
        }
        List<Shape> shapes = new List<Shape>();
        bool IsShapeStart = true;
        Point Shapestart;
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) +";"+ Convert.ToString(e.Y);
        }
        public class Krest:Shape
        {
            int x, y;
            Pen p = new Pen(Color.Black);
            public Krest(int _x,int _y)
            {
                this.x = _x;
                this.y = _y;
            }
            public override void DrawWith(Graphics g)
            {
                g.DrawLine(p, this.x - 5, this.y, this.x + 5, this.y);
                g.DrawLine(p, this.x, this.y-5, this.x, this.y+5);
            }
        }
        public class Line : Shape
        {
            Point s, f;
            Pen p = new Pen(Color.Black);
            public Line(Point _s, Point _f)
            {
                this.s = _s;
                this.f = _f;
            }
            public override void DrawWith(Graphics g)
            {
                g.DrawLine(p, s, f);
            }
        }

        private void MainScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (rb1.Checked)
            {
                shapes.Add(new Krest(e.X, e.Y));
            }
            if (rb2.Checked)
            {
                if (IsShapeStart)
                {
                    Shapestart = e.Location;
                }
                else shapes.Add(new Line(Shapestart,e.Location));
                IsShapeStart = !IsShapeStart;
            }
            this.Refresh();
        }

        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape cr in this.shapes)
                cr.DrawWith(e.Graphics);
        }

        private void rb_change(object sender, EventArgs e)
        {
            IsShapeStart = true;
        }
    }
}
