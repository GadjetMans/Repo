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
        string file = " ";
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) +";"+ Convert.ToString(e.Y);
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
