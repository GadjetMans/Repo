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
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = Convert.ToString(e.X) +";"+ Convert.ToString(e.Y);
        }
        public class Krest
        {
            int x, y;
            Pen p = new Pen(Color.Black);
            public Krest(int _x,int _y)
            {
                this.x = _x;
                this.y = _y;
            }
            public void DrawWith(Graphics g)
            {
                g.DrawLine(p, this.x - 5, this.y, this.x + 5, this.y);
                g.DrawLine(p, this.x, this.y-5, this.x, this.y+5);
            }
        }
    }
}
