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
        private int gap;
        private int rows;
        private int width;
        private NodeMaze start;
        private NodeMaze end;
        private bool started = false;
        Vector2 pos;

        List<List<NodeMaze>> grid;

        public Maze(int rows, int width)
        {
            gap = (int)MazeForm.formSize.X / width;
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
            //O(n) linear time as it has to go through each element in the came from dictionary
            //while the came from contains the key current
            while (came_from.ContainsKey(current))
            {
                //current equals the came_from current
                current = came_from[current];
                //current will make a path for each node added to came from to show the path
                current.make_path();
            }
        }

        private void AStarAlgorithm()
        {
            //make sure start and end node are not null before trying calculation
            if (start != null && end != null) // O(1)
            {
                //start the count at 0
                int count = 0;

                //create the open_set prirority queue with a parametor value of true to get min value back at dequeue
                PriorityQueue<object> open_set = new PriorityQueue<object>(true);

                //add 0 to the open_set with the start node as start is already at start/ 0 distance
                open_set.Enqueue(0, start);

                //create a dictionary which will cotain nodes as nodes and keys
                //this will provide a path back to all the nodes visited so we can provide show the shortest path
                Dictionary<NodeMaze, NodeMaze> came_from = new Dictionary<NodeMaze, NodeMaze>();

                //create another dictionary which will hold the g_score/distance from start node
                Dictionary<NodeMaze, float> g_score = new Dictionary<NodeMaze, float>();

                //create another dictionary which will hold the f_score/addidtion of g_score and heuristic value
                Dictionary<NodeMaze, float> f_score = new Dictionary<NodeMaze, float>();

                //add each node in the maze to the g_score and f_Score which infinity numbers or just very large numbers
                //polynomial complexity O(n2), however only if each node has equal amount of nodes comapred to nodes in grid
                //otherwise, its O(a * b) where a is node lists in grid and b is each node in each list
                foreach (List<NodeMaze> node in this.grid)
                {
                    foreach (NodeMaze node1 in node)
                    {
                        g_score.Add(node1, 1000000000000);
                        f_score.Add(node1, 1000000000000);
                    }
                }

                //remove the start value as this should have a value of 0
                // O(1)
                g_score.Remove(start);
                //add it back with the value of 0
                // O(1)
                g_score.Add(start, 0);

                //remove the start value as this should have a value of 0 and the heuristic value to end pos
                //we can work this out now
                // O(1)
                f_score.Remove(start);
                // O(1)
                f_score.Add(start, heuristic(start.get_pos(), end.get_pos()));

                //while the open set at least contains a value, if it doesnt, it prob hasent found a path to the end
                //worst case is every neighbour is considered without reaching destination
                //if input was 8 nodes with four neighbours each, it would need to do four inner loops for each node, thus, 32 operations

                while (open_set.Count != 0)
                {
                    //the current node equals the node with the shortest f_score in the open_set using priorty queue
                    NodeMaze current = (NodeMaze)open_set.Dequeue();

                    //if the current equals the end, we have reached the end, no need to work out neighbours of end
                    //calcuate the path based on the came_from dictionary and end node
                    //turn the end node into end
                    //break from the while loop
                    // O(1)
                    if (current == end)
                    {
                        reconstruct_path(came_from, end);
                        end.make_end();
                        break;
                    }

                    //O(n)
                    //foreach neighbour of the current node, maximum should be four, could be zero as minimum
                    foreach (NodeMaze neighbour in current.neighbours)
                    {
                        //temp store the g_score plus the gap of the maze as we need to consider this gap between the nodes
                        float temp_g_score = g_score[current] + gap;

                        //if the temp g_score is lower than the g_score of the neighbour
                        if (temp_g_score < g_score[neighbour])
                        {
                            //add the neighbour to the came from dictionary as the current
                            came_from[neighbour] = current;
                            //the g_score of the neighbour will now equal the temp_g_score
                            g_score[neighbour] = temp_g_score;
                            //the f_Score of the neighbour will now equal the temo_g_score + the heuristic value to get to the end node
                            f_score[neighbour] = temp_g_score + heuristic(neighbour.get_pos(), end.get_pos());

                            //if the open set doesnt already contain the neighbour, add the neighbour
                            if (!open_set.IsInQueue(neighbour))
                            {
                                count += 1;
                                //enqueue the neighbour with its f_score
                                open_set.Enqueue((int)f_score[neighbour], neighbour);
                                neighbour.make_open();
                            }
                        }
                    }

                    //if the current is not equal to the start, make it closed to indicate we have cheked this
                    //remember, once this node is closed, it will not be considered as a neighbour to any other nodes
                    if (current != start)
                    {
                        current.make_closed();
                    }
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
                PointF endLineI = new PointF(width * gap, i * gap);

                e.Graphics.DrawLine(pen, startLineI, endLineI);
            }

            //vertical lines with column index
            for (int j = 0; j < rows; j++)
            {
                PointF startLineJ = new PointF(j * gap, 0);
                PointF endLineJ = new PointF(j * gap, width * gap);

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

        public void get_mouse_click(MouseEventArgs mouseEventArgs, string drawOption)
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

                    if(drawOption == DrawOptions.START.ToString() && start == null)
                    {
                        start = nodeMaze;
                        start.make_start();
                    }

                    else if (drawOption == DrawOptions.END.ToString() && end == null)
                    {
                        end = nodeMaze;
                        end.make_end();
                    }

                    else if(drawOption == DrawOptions.BUILDING.ToString())
                    {
                        if (nodeMaze.is_end() == false && nodeMaze.is_start() == false)
                        {
                            nodeMaze.make_barrier();
                            grid[(int)pos.X][(int)pos.Y] = nodeMaze;
                        }
                    }

                    else if (drawOption == DrawOptions.ROAD.ToString())
                    {
                        if (nodeMaze.is_end() == false && nodeMaze.is_start() == false)
                        {
                            nodeMaze.make_road();
                        }
                    }

                    else if (drawOption == DrawOptions.DELETE.ToString())
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
            }

            catch
            {

            }
        }

        public void AStarcommandSent()
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

        public void ClearCommandSent()
        {
            start = null;
            end = null;
            grid = make_grid(rows, width);
        }
    }
}
