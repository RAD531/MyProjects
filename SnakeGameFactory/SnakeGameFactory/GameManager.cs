using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameFactory
{
    class GameManager
    {
        private Vector playableArea;
        public readonly int minX;
        public readonly int minY;

        public GameManager()
        {
            minX = 0;
            minY = 40;

            playableArea = new Vector(400, 400);
        }

        public Vector getPlayabaleArea()
        {
            return playableArea;
        }
    }
}
