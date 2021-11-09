using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    //concrete component, espresso spelt wrong...
    class Expresso : Coffee
    {
        public Expresso()
        {
            this.coffeeName = "Espresso";
        }
    }
}
