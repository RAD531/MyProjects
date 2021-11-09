using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGameFactory
{
    class ConcreteEnemy : IEnemy
    {
        private int i;
        private Vector body;
        List<string> EnemyMove = new List<string> { "left", "right", "up", "down" };
        Random randomMovement = new Random();
        GameManager gameManager = new GameManager();

        public ConcreteEnemy()
        {
            body = new Vector(50, 50);
        }

        public void CollidedWithWall()
        {
            if (body.getSetX < gameManager.minX)
            {
                body.getSetX = gameManager.minX;
            }

            else if (body.getSetX > gameManager.getPlayabaleArea().getSetX)
            {
                body.getSetX = gameManager.getPlayabaleArea().getSetX;
            }

            if (body.getSetY < gameManager.minY)
            {
                body.getSetY = gameManager.minY;
            }

            else if (body.getSetY > gameManager.getPlayabaleArea().getSetY)
            {
                body.getSetY = gameManager.getPlayabaleArea().getSetY;
            }
        }

        public void Draw(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Red, this.body.getSetX, this.body.getSetY, 10, 10);
        }

        public Vector GetBodyLocation()
        {
            return body;
        }

        public string GetName()
        {
            return "Concrete Enemy";
        }

        public void Update()
        {
            //create an int value to randomly pick movement
            i = 0;

            i = randomMovement.Next(0, EnemyMove.Count);

            //create a switch case to execute code upon option
            switch (EnemyMove[i].ToString())
            {
                //if the move = left then calculate if the cell to the left is a 1, if not then - 20 to the x cord
                case "left":
                    body.getSetX += -10;
                    break;
                case "right":
                    body.getSetX += 10;
                    break;
                case "up":
                    body.getSetY += -10;
                    break;
                case "down":
                    body.getSetY += 10;
                    break;
            }

            CollidedWithWall();
        }
    }
}
