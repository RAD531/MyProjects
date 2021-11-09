using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeGame
{
    //use a game manager class to share and manage values across classes
    class GameManager
    {
        //player score
        public static int Score;

        //player name
        public static string PlayerName;

        //shared gametimer
        public static Timer gameTimer = new Timer();

        //store random coffees so the customer can pick one
        public static Type[] randomCoffeeType = new Type[]
{
                typeof(Americano), typeof(Cappuccino), typeof(Expresso), typeof(Latte), typeof(Macchiato), typeof(Mocha)
};
        //store random condiments so the customer can pick one
        public static Type[] randomCondiment = new Type[]
        {
                typeof(Caramel), typeof(Chocolate), typeof(Milk), typeof(Sugar)
        };

        public GameManager()
        {
            gameTimer.Interval = 10;
            //start the timer on start
            gameTimer.Enabled = true;
        }
    }
}
