using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    //concrete decorater
    class Milk : Condiment
    {
        public Milk(Coffee coffee) : base(coffee)
        {
            this.condimentName = "Milk";
        }

        protected override Coffee addCondiment()
        {
            this.condiments.Add(this);
            return this.coffee;
        }
    }
}
