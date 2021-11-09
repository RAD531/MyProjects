using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace AStarPathFinding
{
    public class NodeMaze
    {
        //code inspired from https://www.youtube.com/watch?v=JtiK0DOeI4A

        private int row;
        private int col;
        private int x;
        private int y;
        private int width;
        private int total_rows;
        private SolidColorBrush color;
        SolidColorBrush color2;
        public List<NodeMaze> neighbours;

        public NodeMaze()
        {

        }

        public NodeMaze(int row, int col, int width, int total_rows)
        {
            this.row = row;
            this.col = col;
            this.x = row * width;
            this.y = col * width;
            this.color = new SolidColorBrush(Colors.White);
            this.width = width;
            this.total_rows = total_rows;
        }

        public Vector2 get_pos()
        {
            return new Vector2(this.row, this.col);
        }

        //is path closed
        public bool is_closed(NodeMaze nodeMaze)
        {
            color2 = new SolidColorBrush(Colors.Red);
            return this.color.Color == color2.Color;
        }

        //is path free
        public bool is_open(NodeMaze nodeMaze)
        {
            color2 = new SolidColorBrush(Colors.Green);
            return this.color.Color == color2.Color;
        }

        //is obstacle found
        public bool is_barrier()
        {
            color2 = new SolidColorBrush(Colors.Black);
            return this.color.Color == color2.Color;
        }

        //is start position 
        public bool is_start(NodeMaze nodeMaze)
        {
            color2 = new SolidColorBrush(Colors.Orange);
            return this.color.Color == color2.Color;
        }

        //is the end node found
        public bool is_end(NodeMaze nodeMaze)
        {
            color2 = new SolidColorBrush(Colors.Turquoise);
            return this.color.Color == color2.Color;
        }

        //reset node colour
        public void reset()
        {
            color2 = new SolidColorBrush(Colors.White);
            this.color = color2;
        }

        //=========================================
        //SET NODES
        //=========================================

        //is path closed
        public void make_closed()
        {
            color2 = new SolidColorBrush(Colors.Red);
            this.color = color2;
        }

        //is path free
        public void make_open()
        {
            color2 = new SolidColorBrush(Colors.Green);
            this.color = color2;
        }

        //is obstacle found
        public void make_barrier()
        {
            color2 = new SolidColorBrush(Colors.Black);
            this.color = color2;
        }

        //is start position 
        public void make_start()
        {
            color2 = new SolidColorBrush(Colors.Orange);
            this.color = color2;
        }

        //is the end node found
        public void make_end()
        {
            color2 = new SolidColorBrush(Colors.Turquoise);
            this.color = color2;
        }

        public void make_path()
        {
            color2 = new SolidColorBrush(Colors.Purple);
            this.color = color2;
        }

        public void draw(object sender, PaintEventArgs e)
        {
            var drawingcolor = System.Drawing.Color.FromArgb(this.color.Color.A, this.color.Color.R, this.color.Color.G, this.color.Color.B);
            SolidBrush solidBrush = new SolidBrush(drawingcolor);

            e.Graphics.FillRectangle(solidBrush, this.x, this.y, this.width, this.width);
        }


        public void update_neighbors(List<List<NodeMaze>> grid)
        {
            this.neighbours = new List<NodeMaze>();

            //check if row down from node has neighbors
            //if try to check row down despite on last row, it will crash program
            //also check if barrier, dont add neighbour if barrier


            //DOWN
            if (this.row < this.total_rows - 1 && !grid[this.row + 1][this.col].is_barrier())
            {
                this.neighbours.Add(grid[this.row + 1][this.col]);
            }


            //UP
            if (this.row > 0 && !grid[this.row - 1][this.col].is_barrier())
            {
                this.neighbours.Add(grid[this.row - 1][this.col]);
            }


            //RIGHT
            if (this.col < this.total_rows - 1 && !grid[this.row][this.col + 1].is_barrier())
            {
                this.neighbours.Add(grid[this.row][this.col + 1]);
            }


            //LEFT
            if (this.col > 0 && !grid[this.row][this.col - 1].is_barrier())
            {
                this.neighbours.Add(grid[this.row][this.col - 1]);
            }
        }
    }
}
