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
        public abstract void DrawWith(Graphics g);
        public abstract void SaveTo(StreamWriter sw);
    }
    public class Krest : Shape
    {
        int x, y;
        Pen p = new Pen(Color.Black);
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
        public override void DrawWith(Graphics g)
        {
            g.DrawLine(p, this.x - 5, this.y, this.x + 5, this.y);
            g.DrawLine(p, this.x, this.y - 5, this.x, this.y + 5);
        }
        public override void SaveTo(StreamWriter sw)
        {
            sw.WriteLine("Krest");
            sw.WriteLine(Convert.ToString(this.x) + " " + Convert.ToString(this.y));
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
}
