using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGameFactory
{
    interface ISnake
    {
        string GetName();

        void SetDir(int x, int y);

        void Update();

        void Show(PaintEventArgs e);

        bool EatFood(IFood food);

        void CollidWithEnemy(IEnemy enemy);

        void Grow();

        bool EndGame();
    }
}
