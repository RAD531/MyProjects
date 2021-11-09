using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGameFactory
{
    class ConcreteSnake : ISnake
    {
        private List<Vector> body = new List<Vector>();
        private int xDir;
        private int yDir;
        GameManager gameManager = new GameManager();

        //constructor
        public ConcreteSnake()
        {
            this.body.Add(new Vector(0, 40));
            xDir = 0;
            yDir = 0;
        }


        public void CollidWithEnemy(IEnemy enemy)
        {
            if (this.body[0].getSetX == enemy.GetBodyLocation().getSetX && this.body[0].getSetY == enemy.GetBodyLocation().getSetY)
            {
                Grow();
            }
        }

        public bool EatFood(IFood food)
        {
            if (this.body[0].getSetX == food.GetFoodLocation().getSetX && this.body[0].getSetY == food.GetFoodLocation().getSetY)
            {
                //Intro.Score++;
                this.Grow();
                return true;
            }

            else
            {
                return false;
            }
        }

        public bool EndGame()
        {
            if (this.body.Count > 1)
            {
                for (int i = 1; i < this.body.Count; i++)
                {
                    if (this.body[0].getSetX == this.body[i].getSetX && this.body[0].getSetY == this.body[i].getSetY)
                    {
                        return true;
                    }
                }
            }

            if (this.body[0].getSetX > gameManager.getPlayabaleArea().getSetX || this.body[0].getSetX < gameManager.minX || this.body[0].getSetY > gameManager.getPlayabaleArea().getSetY || this.body[0].getSetY < gameManager.minY)
            {
                return true;
            }

            return false;
        }

        public string GetName()
        {
            return "Concrete Snake";
        }

        public void Grow()
        {
            this.body.Add(new Vector(this.body.First().getSetX, this.body.First().getSetY));
        }

        public void SetDir(int x, int y)
        {
            this.xDir = x;
            this.yDir = y;
        }

        public void Show(PaintEventArgs e)
        {
            for (int i = 0; i < this.body.Count; i++)
            {
                e.Graphics.FillRectangle(Brushes.Green, this.body[i].getSetX, this.body[i].getSetY, 10, 10);
            }
        }

        public void Update()
        {
            if (this.body.Count > 1)
            {
                for (int i = this.body.Count - 1; i >= 0; i--)
                {
                    if (i > 0)
                    {
                        this.body[i].getSetX = this.body[i - 1].getSetX;
                        this.body[i].getSetY = this.body[i - 1].getSetY;
                    }
                }
            }

            this.body[0].getSetX += this.xDir;
            this.body[0].getSetY += this.yDir;
        }
    }
}
