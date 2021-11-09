using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    class Director
    {
        //has a builder object/composistion to make use of the methods.
        //programming to an interface
        Builder builder;

        public Director(Builder builder)
        {
            //director requires builder passed in
            this.builder = builder;
        }

        public void makeCustomer()
        {
            //uses the builder methods
            //first method resets the builder so it wont use previuos implementation
            builder.Reset();
            builder.DrawPictureBox();
            builder.DrawProgressBar();
            builder.orderGenerator();
            builder.SetProgressBar(3000);
        }

    }
}
