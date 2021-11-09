﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    //concrete decorater
    class Sugar : Condiment
    {
        public Sugar(Coffee coffee) : base(coffee)
        {
            this.condimentName = "Sugar";
        }
        protected override Coffee addCondiment()
        {
            this.condiments.Add(this);
            return this.coffee;
        }
    }
}
