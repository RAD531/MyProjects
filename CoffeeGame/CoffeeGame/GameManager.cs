using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeGame
{
    class GameManager
    {
        public static int Score;

        public static string PlayerName;

        public static Timer gameTimer = new Timer();

        public GameManager()
        {
            gameTimer.Interval = 10;
            //start the timer on start
            gameTimer.Enabled = true;
        }
    }
}
