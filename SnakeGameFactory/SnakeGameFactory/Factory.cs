using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameFactory
{
    class Factory
    {
        public ISnake createSnake(string snake)
        {
            if (snake == "Concrete Snake")
            {
                return new ConcreteSnake();
            }

            return null;
        }

        public IFood createFood(string food)
        {
            if (food == "Concrete Food")
            {
                return new ConcreteFood();
            }

            return null;
        }

        public IEnemy createEnemy(string enemy)
        {
            if (enemy == "Concrete Enemy")
            {
                return new ConcreteEnemy();
            }

            return null;
        }

    }
}
