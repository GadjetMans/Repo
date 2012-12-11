using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Repo
{
    public partial class MainScreen : Form
    {
        public abstract class Shape
        {
            public abstract void DrawWith(Graphics g);
            public abstract void SaveTo(StreamWriter sw);
        }
        List<Shape> shapes = new List<Shape>();
        bool IsShapeStart = true;
        Point Shapestart;
        string file = " ";
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
            public Krest(StreamReader sr)
            {
                string line = sr.ReadLine();
                string[] foo = line.Split(' ');
                this.x = Convert.ToInt32(foo[0]);
                this.y = Convert.ToInt32(foo[1]);
            }
            public override void DrawWith(Graphics g)
            {
                g.DrawLine(p, this.x - 5, this.y, this.x + 5, this.y);
                g.DrawLine(p, this.x, this.y-5, this.x, this.y+5);
            }
            public override void SaveTo(StreamWriter sw)
            {
                sw.WriteLine("Krest");
                sw.WriteLine(Convert.ToString(this.x)+" "+Convert.ToString(this.y));
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
            public Line(StreamReader sr)
            {
                string line = sr.ReadLine();
                string line1 = sr.ReadLine();
                string[] foo = line.Split(' ');
                string[] foo1 = line1.Split(' ');
                s.X = Convert.ToInt32(foo[0]);
                s.Y = Convert.ToInt32(foo[1]);
                f.X = Convert.ToInt32(foo1[0]);
                f.Y = Convert.ToInt32(foo1[1]);
            }
            public override void DrawWith(Graphics g)
            {
                g.DrawLine(p, s, f);
            }
            public override void SaveTo(StreamWriter sw)
            {
                sw.WriteLine("Line");
                sw.WriteLine(Convert.ToString(s.X) + " " + Convert.ToString(s.Y));
                sw.WriteLine(Convert.ToString(f.X) + " " + Convert.ToString(f.Y));
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

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
                StreamReader sr = new StreamReader(file);
                while (!sr.EndOfStream)
                {
                    string type = sr.ReadLine();
                    switch (type)
                    {
                        case "Krest":
                            shapes.Add(new Krest(sr));
                            break;
                        case "Line":
                            shapes.Add(new Line(sr));
                            break;
                    }
                }
                this.Refresh();
            }
        }

        private void сохранитькакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = saveFileDialog1.FileName; ;
                StreamWriter sw = new StreamWriter(file);
                foreach (Shape p in this.shapes)
                {
                    p.SaveTo(sw);
                }
                sw.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file = null;
            shapes.Clear();
            this.Refresh();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(file);
            foreach (Shape p in this.shapes)
            {
                p.SaveTo(sw);
            }
            sw.Close();
        }
    }
}
