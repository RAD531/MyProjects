using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeGame
{
    class CustomerBuilder : Builder
    {
        private Customer customer;

        public void DrawPictureBox()
        {
            customer.GetPictureBox().Location = new Point(Coordinates.GetInstance()[0].Item1.X, Coordinates.GetInstance()[0].Item1.Y);
            customer.GetPictureBox().Image = Properties.Resources.Cust;

            //make the picture box transparent
            customer.GetPictureBox().BackColor = Color.Transparent;
            //assign new size
            customer.GetPictureBox().Size = new Size(30, 30);
            //make the image fit to the picture box new size
            customer.GetPictureBox().SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void DrawProgressBar()
        {
            //set up the progress bar control with size, location and value
            customer.GetProgressBar().Visible = true;
            customer.GetProgressBar().Size = new Size(30, 10);
        }

        public void orderGenerator()
        {
            FactoryCoffee factoryCoffee = new FactoryCoffee();
            customer.setCustomerOrder(this, factoryCoffee.returnRandomCoffee());
        }

        public void Reset()
        {
            this.customer = new Customer(this);
        }

        public void SetProgressBar(int progressBarValue)
        {
            customer.GetProgressBar().Maximum = progressBarValue;
            customer.GetProgressBar().Value = progressBarValue;
            customer.SetCutomerInc(this, progressBarValue);
        }

        public Customer GetCustomer()
        {
            Customer result = this.customer;

            return result;
        }
    }
}
