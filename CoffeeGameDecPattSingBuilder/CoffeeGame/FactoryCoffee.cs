using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeGame
{
    class FactoryCoffee
    {
        private List<Type> coffees = new List<Type>();
        private List<Type> condiments = new List<Type>();
        private Coffee coffee;

        public FactoryCoffee()
        {
            coffees.Add(typeof(Americano));
            coffees.Add(typeof(Cappuccino));
            coffees.Add(typeof(Expresso));
            coffees.Add(typeof(Latte));
            coffees.Add(typeof(Macchiato));
            coffees.Add(typeof(Mocha));

            condiments.Add(typeof(Caramel));
            condiments.Add(typeof(Chocolate));
            condiments.Add(typeof(Milk));
            condiments.Add(typeof(Sugar));
        }


        public Coffee returnRandomCoffee()
        {
            //set up new random for the number of items
            Random orderquan = new Random();
            //int to hold the maximum allowed items
            int maximumOrder = 0;

            //set up random for the order item
            Random order = new Random();

            //can order three items for level 3
            maximumOrder = orderquan.Next(1, 4);


            var chosenCofeeIndex = orderquan.Next(coffees.Count());

            switch (chosenCofeeIndex)
            {
                case 0:
                    coffee = new Americano();
                    break;
                case 1:
                    coffee = new Cappuccino();
                    break;
                case 2:
                    coffee = new Expresso();
                    break;
                case 3:
                    coffee = new Latte();
                    break;
                case 4:
                    coffee = new Macchiato();
                    break;
                case 5:
                    coffee = new Mocha();
                    break;
            }

            //start loop to add required number of items
            for (int i = 0; i < maximumOrder; i++)
            {
                var chosenCondimentIndex = orderquan.Next(condiments.Count());

                switch (chosenCondimentIndex)
                {
                    case 0:
                        coffee = new Caramel(coffee);
                        break;
                    case 1:
                        coffee = new Chocolate(coffee);
                        break;
                    case 2:
                        coffee = new Milk(coffee);
                        break;
                    case 3:
                        coffee = new Sugar(coffee);
                        break;
                }
            }

            return coffee;
        }

        public Americano returnAmericano()
        {
            return new Americano();
        }

        public Cappuccino returnCappccino()
        {
            return new Cappuccino();
        }

        public Expresso returnExpresso()
        {
            return new Expresso();
        }

        public Latte returnLate()
        {
            return new Latte();
        }

        public Macchiato returnMacchiato()
        {
            return new Macchiato();
        }

        public Mocha returnMocha()
        {
            return new Mocha();
        }

        public Caramel returnCaramel(Coffee coffee)
        {
            return new Caramel(coffee);
        }

        public Chocolate returnChocolate(Coffee coffee)
        {
            return new Chocolate(coffee);
        }
        public Milk returnMilk(Coffee coffee)
        {
            return new Milk(coffee);
        }

        public Sugar returnSugar(Coffee coffee)
        {
            return new Sugar(coffee);
        }
    }
}
