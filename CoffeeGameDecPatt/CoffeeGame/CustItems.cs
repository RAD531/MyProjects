using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    //need a class where classes can grab these shared values
    class CustItems
    {
        public static Customer? customerID { get; set; }

        public static Coffee? custCoffee { get; set; }
    }
}
