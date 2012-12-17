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
        List<Shape> shapes = new List<Shape>();
        bool IsShapeStart = true;
        Point Shapestart;
        Pen pMain = new Pen(Color.Black);
        Pen pTemp = new Pen(Color.Red);
        Shape tempshape;
        string file = " ";
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (rb1.Checked)
            {
                tempshape = (new Krest(e.X, e.Y));
            }
            if (rb2.Checked)
            {
                if (!IsShapeStart)
                {
                    tempshape = new Line(Shapestart, e.Location);
                }
            }
            if (rb3.Checked)
            {
                if (!IsShapeStart)
                {
                    tempshape = new Circle(Shapestart, e.Location);
                }
            }
            if (rb4.Checked)
            {
                if (!IsShapeStart)
                {
                    tempshape = new Ellipse(Shapestart, e.Location);
                }
            }
            this.Refresh();
        }
        private void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }
        private void MainScreen_MouseDown(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) + ";" + Convert.ToString(e.Y);
            if (rb1.Checked)
            {
                AddShape(tempshape);
            }
            if (rb2.Checked)
            {
                if (IsShapeStart)
                {
                    Shapestart = e.Location;
                }
                else AddShape(tempshape);
                IsShapeStart = !IsShapeStart;
            }
            if (rb3.Checked)
            {
                if (IsShapeStart)
                {
                    Shapestart = e.Location;
                }
                else AddShape(tempshape);
                IsShapeStart = !IsShapeStart;
            }
            if (rb4.Checked)
            {
                if (IsShapeStart)
                {
                    Shapestart = e.Location;
                }
                else AddShape(tempshape);
                IsShapeStart = !IsShapeStart;
            }
            this.Refresh();
        }

        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            if (tempshape != null)
            {
                tempshape.DrawWith(e.Graphics, pTemp);
            }
            foreach (Shape cr in this.shapes)
                cr.DrawWith(e.Graphics,pMain);
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
                        case "Circle":
                            shapes.Add(new Circle(sr));
                            break;
                        case "Ellipse":
                            shapes.Add(new Ellipse(sr));
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

        private void MainScreen_MouseLeave(object sender, EventArgs e)
        {
            tempshape = null;
            this.Refresh();
        }
    }
}
