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
    public abstract class Shape
    {
        public abstract void DrawWith(Graphics g, Pen p);
        public abstract void SaveTo(StreamWriter sw);
        public abstract string Coord_Str { get; }
    }
    public class Krest : Shape
    {
        int x, y;
        public Krest(int _x, int _y)
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
        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawLine(p, this.x - 5, this.y, this.x + 5, this.y);
            g.DrawLine(p, this.x, this.y - 5, this.x, this.y + 5);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Krest");
            sw.WriteLine(Convert.ToString(this.x) + " " + Convert.ToString(this.y));
        }
        public override string Coord_Str
        {
            get { return ("Krest(" + Convert.ToString(x) + ";" + Convert.ToString(y) + ")"); }
        }
    }
    public class Line : Shape
    {
        Point s, f;
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
        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawLine(p, s, f);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Line");
            sw.WriteLine(Convert.ToString(s.X) + " " + Convert.ToString(s.Y));
            sw.WriteLine(Convert.ToString(f.X) + " " + Convert.ToString(f.Y));
        }
        public override string Coord_Str
        {
            get { return ("Line(" + Convert.ToString(s.X) + ";" + Convert.ToString(s.Y) + ")-("+Convert.ToString(f.X)+";"+Convert.ToString(f.Y)+")"); }
        }
    }
    public class Circle : Shape
    {
        Point c, a;
        float r;
        public Circle(Point _c, Point _a)
        {
            this.c = _c;
            this.a = _a;
        }
        public Circle(StreamReader sr)
        {
            string line = sr.ReadLine();
            string line1 = sr.ReadLine();
            string[] foo = line.Split(' ');
            string[] foo1 = line1.Split(' ');
            c.X = Convert.ToInt32(foo[0]);
            c.Y = Convert.ToInt32(foo[1]);
            a.X = Convert.ToInt32(foo1[0]);
            a.Y = Convert.ToInt32(foo1[1]);
        }
        public override void DrawWith(Graphics g, Pen p)
        {
            r = (float)Math.Sqrt(Math.Pow((a.X - c.X), 2) + Math.Pow((a.Y - c.Y), 2));
            g.DrawEllipse(p, c.X - r, c.Y - r, 2 * r, 2 * r);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Circle");
            sw.WriteLine(Convert.ToString(c.X) + " " + Convert.ToString(c.Y));
            sw.WriteLine(Convert.ToString(a.X) + " " + Convert.ToString(a.Y));
        }
        public override string Coord_Str
        {
            get { return ("Circle(" + Convert.ToString(c.X) + ";" + Convert.ToString(c.Y) + ";r=" + Convert.ToString(r) + ")"); }
        }
    }
    public class Ellipse : Shape
    {
        Point b, a;
        public Ellipse(Point _a, Point _b)
        {
            this.a = new Point(Math.Min(_a.X, _b.X), Math.Min(_a.Y, _b.Y));
            this.b = new Point(Math.Max(_a.X, _b.X), Math.Max(_a.Y, _b.Y));
        }
        public Ellipse(StreamReader sr)
        {
            string line = sr.ReadLine();
            string line1 = sr.ReadLine();
            string[] foo = line.Split(' ');
            string[] foo1 = line1.Split(' ');
            a.X = Convert.ToInt32(foo[0]);
            a.Y = Convert.ToInt32(foo[1]);
            b.X = Convert.ToInt32(foo1[0]);
            b.Y = Convert.ToInt32(foo1[1]);
        }
        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawEllipse(p, a.X, a.Y, width, height);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Ellipse");
            sw.WriteLine(Convert.ToString(a.X) + " " + Convert.ToString(a.Y));
            sw.WriteLine(Convert.ToString(b.X) + " " + Convert.ToString(b.Y));
        }
        public override string Coord_Str
        {
            get { return ("Ellipse(" + Convert.ToString(a.X) + ";" + Convert.ToString(a.Y) + ";w=" + Convert.ToString(Math.Abs(width)) + ";h=" + Convert.ToString(Math.Abs(height)) + ")"); }
        }
        public float width
        {
            get
            {
                return (b.X - a.X);
            }
        }
        public float height
        {
            get
            {
                return (b.Y - a.Y);
            }
        }
    }
    public class Rectangle : Shape
    {
        Point a, b;
        public Rectangle(Point _a, Point _b)
        {
            this.a = new Point(Math.Min(_a.X, _b.X), Math.Min(_a.Y, _b.Y));
            this.b = new Point(Math.Max(_a.X, _b.X), Math.Max(_a.Y, _b.Y));
        }
        public Rectangle(StreamReader sr)
        {
            string line = sr.ReadLine();
            string line1 = sr.ReadLine();
            string[] foo = line.Split(' ');
            string[] foo1 = line1.Split(' ');
            a.X = Convert.ToInt32(foo[0]);
            a.Y = Convert.ToInt32(foo[1]);
            b.X = Convert.ToInt32(foo1[0]);
            b.Y = Convert.ToInt32(foo1[1]);
        }
        public override void DrawWith(Graphics g, Pen p)
        {
            g.DrawRectangle(p, a.X, a.Y, width, height);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Rectangle");
            sw.WriteLine(Convert.ToString(a.X) + " " + Convert.ToString(a.Y));
            sw.WriteLine(Convert.ToString(b.X) + " " + Convert.ToString(b.Y));
        }
        public override string Coord_Str
        {
            get { return ("Rectangle(" + Convert.ToString(a.X) + ";" + Convert.ToString(a.Y) + ";w=" + Convert.ToString(Math.Abs(width)) + ";h=" + Convert.ToString(Math.Abs(height)) + ")"); }
        }
        public float width
        {
            get
            {
                return (b.X - a.X);
            }
        }
        public float height
        {
            get
            {
                return (b.Y - a.Y);
            }
        }
    }
}
