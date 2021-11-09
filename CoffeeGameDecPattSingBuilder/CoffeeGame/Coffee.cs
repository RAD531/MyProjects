using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    //base class/component
    public abstract class Coffee
    {
        public string coffeeName;
        public List<Condiment> condiments = new List<Condiment>();
    }
}
