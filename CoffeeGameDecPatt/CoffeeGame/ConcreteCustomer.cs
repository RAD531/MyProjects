using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CoffeeGame
{
    //create a new concrete customer class in case you ever need it, there might be variants of customers
    class ConcreteCustomer : Customer
    {
        public override void SetPictureBox(Bitmap image)
        {
            base.SetPictureBox(image);
        }
    }
}
