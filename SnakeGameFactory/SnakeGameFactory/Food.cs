using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGameFactory
{
    interface IFood
    {
        Vector GetFoodLocation();

        string GetName();

        void SetFoodLocation();

        void Draw(PaintEventArgs e);
    }
}
