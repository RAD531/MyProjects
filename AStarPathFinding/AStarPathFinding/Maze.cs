using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarPathFinding
{
    public class Maze
    {
        private int gap = 10;
        private int rows;
        private int width;
        private NodeMaze start;
        private NodeMaze end;
        private bool started = false;
        Vector2 pos;

        List<List<NodeMaze>> grid;

        public Maze(int rows, int width)
        {
            this.rows = rows;
            this.width = width;
            make_grid(rows, width);
        }

        //prediction of distance between point 1 and 2
        private int heuristic(Vector2 p1, Vector2 p2)
        {
            int x1 = (int)p1.X; 
            int y1 = (int)p1.Y;
            int x2 = (int)p2.X;
            int y2 = (int)p2.Y;

            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        private void reconstruct_path(Dictionary<NodeMaze, NodeMaze> came_from, NodeMaze current)
        {
            while (came_from.ContainsKey(current))
            {
                current = came_from[current];
                current.make_path();
            }
        }

        public void AStarAlgorithm()
        {
            int count = 0;

            PriorityQueue<object> open_set = new PriorityQueue<object>(true);

            open_set.Enqueue(0, start);

            Dictionary<NodeMaze, NodeMaze> came_from = new Dictionary<NodeMaze, NodeMaze>();

            Dictionary<NodeMaze, float> g_score = new Dictionary<NodeMaze, float>();

            Dictionary<NodeMaze, float> f_score = new Dictionary<NodeMaze, float>();

            foreach (List<NodeMaze> node in this.grid)
            {
                foreach (NodeMaze node1 in node)
                {
                    g_score.Add(node1, 1000000000000);
                    f_score.Add(node1, 1000000000000);
                }
            }

            g_score.Remove(start);
            g_score.Add(start, 0);

            f_score.Remove(start);
            f_score.Add(start, heuristic(start.get_pos(), end.get_pos()));

            while (open_set.Count != 0)
            {
                NodeMaze current = (NodeMaze)open_set.Dequeue();

                if (current == end)
                {
                    reconstruct_path(came_from, end);
                    end.make_end();
                    break;
                }

                foreach(NodeMaze neighbour in current.neighbours)
                {
                    float temp_g_score = g_score[current] + gap;

                    if (temp_g_score < g_score[neighbour])
                    {
                        came_from[neighbour] = current;
                        g_score[neighbour] = temp_g_score;
                        f_score[neighbour] = temp_g_score + heuristic(neighbour.get_pos(), end.get_pos());

                        if (!open_set.IsInQueue(neighbour))
                        {
                            count += 1;
                            open_set.Enqueue((int)f_score[neighbour], neighbour);
                            neighbour.make_open();
                        }
                    }
                }

                if (current != start)
                {
                    current.make_closed();
                }
            }
        } 


        //create matrix of nodes
        private List<List<NodeMaze>> make_grid(int rows, int width)
        {
            grid = new List<List<NodeMaze>>();

            for (int i = 0; i < rows; i++)
            {
                grid.Add(new List<NodeMaze>());

                for (int j = 0; j < rows; j++)
                {
                    NodeMaze nodeMaze = new NodeMaze(i, j, gap, rows);
                    grid[i].Add(nodeMaze);
                }
            }

            return grid;
        }

        //draw grid on screen
        public void draw_grid(object sender, PaintEventArgs e)
        {

            Pen pen = new Pen(Brushes.Gray, 0.1f);

            //horizontal lines with row index
            for (int i = 0; i < rows; i++)
            {
                PointF startLineI = new PointF(0, i * gap);
                PointF endLineI = new PointF(width * 10, i * gap);

                e.Graphics.DrawLine(pen, startLineI, endLineI);
            }

            //vertical lines with column index
            for (int j = 0; j < rows; j++)
            {
                PointF startLineJ = new PointF(j * gap, 0);
                PointF endLineJ = new PointF(j * gap, width * 10);

                e.Graphics.DrawLine(pen, startLineJ, endLineJ);
            }
        }

        public void draw_every_node(object sender, PaintEventArgs e)
        {
            for(int i = 0; i < grid.Count(); i++)
            {
                foreach(NodeMaze nodeMaze in grid[i])
                {
                    nodeMaze.draw(sender, e);
                }
            }
        }

        //get where the user clicked
        public Vector2 hget_cliked_pos(MouseEventArgs mouseEventArgs, int rows, int width)
        {
            int row = mouseEventArgs.Y / gap;
            int col = mouseEventArgs.X / gap;

            pos = new Vector2(col, row);

            return pos;
        }

        public void get_mouse_click(MouseEventArgs mouseEventArgs)
        {
            pos = hget_cliked_pos(mouseEventArgs, rows, width);

            //try and find the position on the grid where the user has clicked
            try
            {
                //map where the user has clicked to the coords on the grid
                NodeMaze nodeMaze = grid[(int)pos.X][(int)pos.Y];

                //if left mouse button has been clicked
                if (mouseEventArgs.Button == MouseButtons.Left)
                {

                    //if start has not been set just yet
                    //change its colour
                    if (start == null && nodeMaze != end)
                    {
                        start = nodeMaze;
                        start.make_start();
                    }

                    //else if end has not been set
                    //change its colour
                    else if (end == null && nodeMaze != start)
                    {
                        end = nodeMaze;
                        end.make_end();
                    }

                    //else if both start and end have been set
                    //draw a barrier
                    else if (nodeMaze != end && nodeMaze != start)
                    {
                        nodeMaze.make_barrier();
                        grid[(int)pos.X][(int)pos.Y] = nodeMaze;
                    }
                }

                else if (mouseEventArgs.Button == MouseButtons.Right)
                {
                    nodeMaze.reset();

                    if (nodeMaze == start)
                    {
                        start = null;
                    }

                    if (nodeMaze == end)
                    {
                        end = null;
                    }
                }

            }

            catch
            {

            }
        }

        public void get_key_down(KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.KeyCode == Keys.Space && started == false)
            {
                for (int i = 0; i < grid.Count(); i++)
                {
                    foreach (NodeMaze nodeMaze in grid[i])
                    {
                        nodeMaze.update_neighbors(grid);
                    }
                }

                AStarAlgorithm();
            }

            if (keyEventArgs.KeyCode == Keys.C)
            {
                start = null;
                end = null;
                grid = make_grid(rows, width);
            }
        }


    }
}
