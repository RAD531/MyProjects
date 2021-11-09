using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGameFactory
{
    public partial class Form1 : Form
    {
        Timer timer;
        ISnake snake;
        IEnemy enemy;
        IFood food;
        Factory factory = new Factory();

        bool eatenFood = false;

        public Form1()
        {
            InitializeComponent();

            snake = factory.createSnake("Concrete Snake");
            enemy = factory.createEnemy("Concrete Enemy");
            food = factory.createFood("Concrete Food");


            food.SetFoodLocation();

            timer = new Timer();

            //fire every 10th of a second/millseconds
            timer.Interval = 100;

            //start the timer on start
            timer.Enabled = true;

            //event handler, every tick
            timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //lblScore.Text = lblScore.Text + " " + Intro.Score.ToString();

            snake.Update();
            enemy.Update();

            //this must be called after the update method as it checks if head is in same pos of body
            //after eaten food, it will be, so it needs update method to change that.
            if (snake.EndGame() == true)
            {
                timer.Stop();

                MessageBox.Show("You have been killed, game over");
                this.Close();
            }

            eatenFood = snake.EatFood(food);

            snake.CollidWithEnemy(enemy);

            if (eatenFood)
            {
                food.SetFoodLocation();
                eatenFood = false;
            }

            //force the form to redraw itself 
            Refresh();
            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            snake.Show(e);
            food.Draw(e);
            enemy.Draw(e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    snake.SetDir(0, -10);
                    break;
                case Keys.Down:
                    snake.SetDir(0, 10);
                    break;
                case Keys.Left:
                    snake.SetDir(-10, 0);
                    break;
                case Keys.Right:
                    snake.SetDir(10, 0);
                    break;
            }
        }
    }
}
