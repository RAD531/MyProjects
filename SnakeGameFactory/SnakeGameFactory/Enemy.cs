using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGameFactory
{
    interface IEnemy
    {
        string GetName();

        Vector GetBodyLocation();

        void Update();

        void CollidedWithWall();

        void Draw(PaintEventArgs e);

}
}
