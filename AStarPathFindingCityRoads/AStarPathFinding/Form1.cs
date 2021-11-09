using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
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
        string drawOption;
        List<ToolStripMenuItem> drawToolStripMenuItems = new List<ToolStripMenuItem>();

        public static Vector2 formSize;

        public MazeForm()
        {
            InitializeComponent();

            formSize = new Vector2(this.Width, this.Height); 

            formTimer = new Timer();
            maze = new Maze(this.Height / 20, this.Width / 20);
            this.AutoSize = false;

            drawToolStripMenuItems.Add(startToolStripMenuItem);
            drawToolStripMenuItems.Add(endToolStripMenuItem);
            drawToolStripMenuItems.Add(buildingToolStripMenuItem);
            drawToolStripMenuItems.Add(roadToolStripMenuItem);
            drawToolStripMenuItems.Add(deleteToolStripMenuItem);

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
                maze.get_mouse_click(e, drawOption);
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
                maze.get_mouse_click(e, drawOption);
            }
        }

        private void MazeForm_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void raodToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_RightToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a A* algorithm program which shows the shortest path through roads and around building drawn from the user. The A* algorithm will only run once a start and end point have been drawn along with the roads used to find the shortest path. The draw options are available by right-clicking, selecting Draw, then selecting the item to draw. When the A* algorithm is run, the map will show orange roads for the shortest path (if found), green roads which are open and red roads which are the roads which were considered in the algorthm before it found the shortest path. The difference with A* and Dijkstra's is that Dijkstra's would have considered all possible paths.");
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawOption = DrawOptions.START.ToString();
            changeChecksStripItem(startToolStripMenuItem);
        }

        private void endToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawOption = DrawOptions.END.ToString();
            changeChecksStripItem(endToolStripMenuItem);
        }

        private void buildingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawOption = DrawOptions.BUILDING.ToString();
            changeChecksStripItem(buildingToolStripMenuItem);
        }

        private void roadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawOption = DrawOptions.ROAD.ToString();
            changeChecksStripItem(roadToolStripMenuItem);
        }

        private void changeChecksStripItem(ToolStripMenuItem selectedToolStripMenuItem)
        {
            foreach(ToolStripMenuItem toolStripMenuItem in drawToolStripMenuItems)
            {
                if (toolStripMenuItem == selectedToolStripMenuItem)
                {
                    if (toolStripMenuItem.Checked == true)
                    {
                        toolStripMenuItem.Checked = false;
                        drawOption = "";
                    }

                    else
                    {
                        toolStripMenuItem.Checked = true;
                    }
                }

                else
                {
                    toolStripMenuItem.Checked = false;
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawOption = DrawOptions.DELETE.ToString();
            changeChecksStripItem(deleteToolStripMenuItem);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            maze.ClearCommandSent();
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            maze.AStarcommandSent();
        }
    }
}
