using AStarPathFinding.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        private Bitmap image;
        private string[] imageTags = { "start", "end", "barrier", "road", "roadComplete", "roadOpen", "roadClosed"};

        public NodeMaze()
        {
            checkImages();
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
            if (this.image != null)
            {
                return this.image.Tag.ToString() == imageTags[6];
            }

            return false;
        }

        //is path free
        public bool is_open(NodeMaze nodeMaze)
        {
            if (this.image != null)
            {
                return this.image.Tag.ToString() == imageTags[5];
            }

            return false;
        }

        public bool is_road()
        {
            if (this.image != null)
            {
                return this.image.Tag.ToString() == imageTags[3];
            }

            return false;
        }

        //is obstacle found
        public bool is_barrier()
        {
            if (this.image != null)
            {
                return this.image.Tag.ToString() == imageTags[2];
            }

            return false;

            //color2 = new SolidColorBrush(Colors.Black);
            //return this.color.Color == color2.Color;
        }

        //is start position 
        public bool is_start()
        {
            if (this.image != null)
            {
                return this.image.Tag.ToString() == imageTags[0];
            }

            return false;

            //color2 = new SolidColorBrush(Colors.Orange);
            //return this.color.Color == color2.Color;
        }

        //is the end node found
        public bool is_end()
        {
            if (this.image != null)
            {
                return this.image.Tag.ToString() == imageTags[1];
            }

            return false;

            //color2 = new SolidColorBrush(Colors.Turquoise);
            //return this.color.Color == color2.Color;
        }

        //reset node colour
        public void reset()
        {
            if (image != null)
            {
                image = null;
            }

            color2 = new SolidColorBrush(Colors.White);
            this.color = color2;
        }

        //=========================================
        //SET NODES
        //=========================================

        //is path closed
        public void make_closed()
        {
            image = new Bitmap(Resources.road_pathClosed, this.width, this.width);
            image.Tag = imageTags[6];
        }

        //is path free
        public void make_open()
        {
            image = new Bitmap(Resources.road_pathOpen, this.width, this.width);
            image.Tag = imageTags[5];
        }

        public void make_road()
        {
            image = new Bitmap(Resources.road, this.width, this.width);
            image.Tag = imageTags[3];
        }

        //is obstacle found
        public void make_barrier()
        {
            //color2 = new SolidColorBrush(Colors.Black);
            //this.color = color2;
            image = new Bitmap(Resources.building, this.width, this.width);
            image.Tag = imageTags[2];
        }

        //is start position 
        public void make_start()
        {
            image = new Bitmap(Resources.carGreen, this.width, this.width);
            image.Tag = imageTags[0];

            //color2 = new SolidColorBrush(Colors.Orange);
            //this.color = color2;
        }

        //is the end node found
        public void make_end()
        {
            image = new Bitmap(Resources._case, this.width, this.width);
            image.Tag = imageTags[1];

            //color2 = new SolidColorBrush(Colors.Turquoise);
            //this.color = color2;
        }

        public void make_path()
        {
            image = new Bitmap(Resources.road_pathComplete, this.width, this.width);
            image.Tag = imageTags[4];

            //color2 = new SolidColorBrush(Colors.Purple);
            //this.color = color2;
        }

        public void draw(object sender, PaintEventArgs e)
        {

            if (image != null)
            {
                e.Graphics.DrawImage(image, this.x, this.y);
            }

            else
            {
                var drawingcolor = System.Drawing.Color.FromArgb(this.color.Color.A, this.color.Color.R, this.color.Color.G, this.color.Color.B);
                SolidBrush solidBrush = new SolidBrush(drawingcolor);

                e.Graphics.FillRectangle(solidBrush, this.x, this.y, this.width, this.width);
            }
        }


        public void update_neighbors(List<List<NodeMaze>> grid)
        {
            this.neighbours = new List<NodeMaze>();

            //check if row down from node has neighbors
            //if try to check row down despite on last row, it will crash program
            //also check if barrier, dont add neighbour if barrier


            //DOWN
            if (this.row < this.total_rows - 1 && !grid[this.row + 1][this.col].is_barrier() && grid[this.row + 1][this.col].is_road() || this.row < this.total_rows - 1 && !grid[this.row + 1][this.col].is_barrier() && grid[this.row + 1][this.col].is_end())
            {
                this.neighbours.Add(grid[this.row + 1][this.col]);
            }


            //UP
            if (this.row > 0 && !grid[this.row - 1][this.col].is_barrier() && grid[this.row - 1][this.col].is_road() || this.row > 0 && !grid[this.row - 1][this.col].is_barrier() && grid[this.row - 1][this.col].is_end())
            {
                this.neighbours.Add(grid[this.row - 1][this.col]);
            }


            //RIGHT
            if (this.col < this.total_rows - 1 && !grid[this.row][this.col + 1].is_barrier() && grid[this.row][this.col + 1].is_road() || this.col < this.total_rows - 1 && !grid[this.row][this.col + 1].is_barrier() && grid[this.row][this.col + 1].is_end())
            {
                this.neighbours.Add(grid[this.row][this.col + 1]);
            }


            //LEFT
            if (this.col > 0 && !grid[this.row][this.col - 1].is_barrier() && grid[this.row][this.col - 1].is_road() || this.col > 0 && !grid[this.row][this.col - 1].is_barrier() && grid[this.row][this.col - 1].is_end())
            {
                this.neighbours.Add(grid[this.row][this.col - 1]);
            }
        }

        private void checkImages()
        {
            //check if resources exist and will be ok to store in Bitmap
            try
            {
                Bitmap tempImage = Resources.building;
                tempImage = Resources.carGreen;
                tempImage = Resources.road;
                tempImage = Resources.road_pathClosed;
                tempImage = Resources.road_pathComplete;
                tempImage = Resources.road_pathOpen;

                tempImage = null;
            }

            //display error to user and exit application
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Application.Exit();
            }
        }
    }
}
