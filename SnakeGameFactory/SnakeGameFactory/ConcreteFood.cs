using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGameFactory
{
    class ConcreteFood : IFood
    {
        Random randomFoodPos;
        Vector foodPos;
        GameManager gameManager = new GameManager();

        public ConcreteFood()
        {
            randomFoodPos = new Random();
            foodPos = new Vector(0, 0);
        }

        public void Draw(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, this.foodPos.getSetX, this.foodPos.getSetY, 10, 10);
        }

        public Vector GetFoodLocation()
        {
            return foodPos;
        }

        public string GetName()
        {
            return "Concrete Food";
        }


        public void SetFoodLocation()
        {
            this.foodPos.getSetX = Convert.ToInt32(randomFoodPos.Next(0, gameManager.getPlayabaleArea().getSetX));
            this.foodPos.getSetY = Convert.ToInt32(randomFoodPos.Next(0, gameManager.getPlayabaleArea().getSetY));

            this.foodPos.getSetX = ((int)Math.Round(foodPos.getSetX / 10.0)) * 10;
            this.foodPos.getSetY = ((int)Math.Round(foodPos.getSetY / 10.0)) * 10;
        }

    }
}
