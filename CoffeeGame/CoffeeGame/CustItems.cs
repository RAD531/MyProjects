using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    class CustItems
    {
        //association
        public static Customer? customerID { get; set; }

        //association
        public static Coffees? cofeeType { get; set; }

        //aggregation/multiplicity
        public static List<Condiments>? condiments { get; set; } = new List<Condiments>();
    }
}
