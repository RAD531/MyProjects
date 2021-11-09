using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameFactory
{
    class Vector
    {
        private int x;
        private int y;

        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int getSetX
        {
            get { return x; }
            set { x = value; }
        }

        public int getSetY
        {
            get { return y; }
            set { y = value; }
        }
    }
}
