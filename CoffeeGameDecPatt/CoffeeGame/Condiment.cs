using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    //base decorater
    public abstract class Condiment : Coffee
    {
        //aggregation
        protected Coffee coffee;

        //Constructor
        public Condiment(Coffee coffee)
        {
            this.coffee = coffee;
            this.coffeeName = coffee.coffeeName;
            this.condiments = coffee.condiments;
            addCondiment();
        }

        public string condimentName;

        protected abstract Coffee addCondiment();
    }
}
