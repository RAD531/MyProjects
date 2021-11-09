using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarPathFinding
{
    public partial class MazeForm : Form
    {
        Timer formTimer;
        Maze maze;
        bool mouseDown = false;

        public MazeForm()
        {
            InitializeComponent();

            formTimer = new Timer();
            maze = new Maze(this.Height / 10, this.Width / 10);
            this.AutoSize = false;

            //fire every 10th of a second/millseconds
            formTimer.Interval = 1;

            formTimer.Enabled = true;

            //event handler, every tick
            formTimer.Tick += formTimer_Tick;
        }

        private void formTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
            this.DoubleBuffered = true;
        }

        private void Maze_Paint(object sender, PaintEventArgs e)
        {
            maze.draw_every_node(this, e);
            maze.draw_grid(this, e);
        }

        private void MazeForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;

            if (mouseDown)
            {
                maze.get_mouse_click(e);
            }
        }

        private void MazeForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void MazeForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                maze.get_mouse_click(e);
            }
        }

        private void MazeForm_KeyDown(object sender, KeyEventArgs e)
        {
            maze.get_key_down(e);
        }
    }
}
