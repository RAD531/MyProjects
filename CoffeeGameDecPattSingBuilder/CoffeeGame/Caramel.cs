using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    //concrete decorater
    class Caramel : Condiment
    {
        public Caramel(Coffee coffee) : base(coffee)
        {
            this.condimentName = "Caramel";
        }

        protected override Coffee addCondiment()
        {
            this.condiments.Add(this);
            return this.coffee;
        }
    }
}
