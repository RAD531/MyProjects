using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    //concrete decorater
    class Chocolate : Condiment
    {
        public Chocolate(Coffee coffee) : base(coffee)
        {
            this.condimentName = "Chocolate";
        }

        protected override Coffee addCondiment()
        {
            this.condiments.Add(this);
            return this.coffee;
        }
    }
}
