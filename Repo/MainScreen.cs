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
        Pen pSel = new Pen(Color.Green, 2);
        Shape tempshape;
        string file = " ";
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (rbCross.Checked)
            {
                tempshape = (new Krest(e.X, e.Y));
            }
            if (!IsShapeStart)
            {
                if (rbLine.Checked) tempshape = new Line(Shapestart, e.Location);
                if (rbCircle.Checked) tempshape = new Circle(Shapestart, e.Location);
                if (rbEllipse.Checked) tempshape = new Ellipse(Shapestart, e.Location);
                if (rbRectangle.Checked) tempshape = new Rectangle(Shapestart, e.Location);
            }

            this.Refresh();
        }
        private void AddShape(Shape shape)
        {
            shapes.Add(shape);
            SList.Items.Add(shape.Coord_Str);
        }
        private void MainScreen_MouseDown(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) + ";" + Convert.ToString(e.Y);
            if (rbCross.Checked)
            {
                AddShape(tempshape);
            }
            else
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
            foreach (int i in SList.SelectedIndices)
                shapes[i].DrawWith(e.Graphics, pSel);
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
                            AddShape(new Krest(sr));
                            break;
                        case "Line":
                            AddShape(new Line(sr));
                            break;
                        case "Circle":
                            AddShape(new Circle(sr));
                            break;
                        case "Ellipse":
                            AddShape(new Ellipse(sr));
                            break;
                        case "Rectangle":
                            AddShape(new Rectangle(sr));
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
                file = saveFileDialog1.FileName;
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
            file = " ";
            shapes.Clear();
            SList.Items.Clear();
            this.Refresh();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file!=" ")
            {
                StreamWriter sw = new StreamWriter(file);
                foreach (Shape p in this.shapes)
                {
                    p.SaveTo(sw);
                }
                sw.Close();
            }
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    file = saveFileDialog1.FileName;
                    StreamWriter sw = new StreamWriter(file);
                    foreach (Shape p in this.shapes)
                    {
                        p.SaveTo(sw);
                    }
                    sw.Close();
                }
            }
        }

        private void MainScreen_MouseLeave(object sender, EventArgs e)
        {
            tempshape = null;
            this.Refresh();
        }

        private void SList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (SList.SelectedIndices.Count > 0)
            {
                shapes.RemoveAt(SList.SelectedIndices[0]);
                SList.Items.RemoveAt(SList.SelectedIndices[0]);
            }
        }
    }
}
