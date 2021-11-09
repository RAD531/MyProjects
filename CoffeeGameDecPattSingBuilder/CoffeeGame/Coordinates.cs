using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Drawing;

namespace CoffeeGame
{
    //thread-safe singleton
    class Coordinates : List<Tuple<Point, bool>>
    {
        //coordiantes for customer movement, true = free space, if false, then customer cant move there
        private Coordinates()
        {

        }

        private static List<Tuple<Point, bool>> coordinates { get; set; } = new List<Tuple<Point, bool>>()
        {
            {new Tuple<Point, bool>(new Point(-100, 112),true )},
            {new Tuple<Point, bool>(new Point(0, 112),true )},
             {new Tuple<Point, bool>(new Point(100, 112),true )},
              {new Tuple<Point, bool>(new Point(200, 112),true )},
               {new Tuple<Point, bool>(new Point(300, 112),true )},
                {new Tuple<Point, bool>(new Point(400, 112),true )},
                 {new Tuple<Point, bool>(new Point(400, 184),true )},
                  {new Tuple<Point, bool>(new Point(400, 232),true )},
                   {new Tuple<Point, bool>(new Point(400, 280),true )}
        };

        private static readonly object _lock = new object();

        public static List<Tuple<Point, bool>> GetInstance()
        {
            if (coordinates == null)
            {
                lock (_lock)
                {
                    if (coordinates == null)
                    {
                        coordinates = new Coordinates();
                    }
                }
            }

            return coordinates;
        }
    }
}
