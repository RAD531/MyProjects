using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CoffeeGame
{
    public class Customer
    {
        //used to decrease customer progress bar
        private int customerInc;
        //make the customer imgage accessible
        private PictureBox customerImg = new PictureBox();
        //same for progress bar
        private ProgressBar customerPB = new ProgressBar();

        private Coffee customerOrderCoffee;

        private bool isMoving = false;

        //adds to user score, gets populated by final progress bar value
        private int customerScore = 0;

        private Point destination;
        //walk back identifies if the customer is walking away from the counter
        private bool walkBack = false;
        //let form 1 know when to dispose of object
        public bool inUse {get;set;}


        public Customer(Builder builder)
        {
            //set values
            this.inUse = true;

            //start the customer timer
            GameManager.gameTimer.Tick += custTimer_Tick;
        }

        protected void custTimer_Tick(object sender, EventArgs e)
        {
            //location of objects
            Location();

            //value of progress bar
            ProgressBarValue();

            //moveCustomer
            moveCustomer();
            
        }

        //======================================
        //PROGRESS BAR
        //======================================
        public ProgressBar GetProgressBar()
        {
            //return the contol to coffee form
            return customerPB;
        }

        public void SetCutomerInc(Builder builder, int progressBarValue)
        {
            customerInc = progressBarValue;
        }

        private void ProgressBarValue()
        {
            if (customerPB.Value >= customerInc)
            {
                //decrease number with each iteration
                customerInc--;
                //progress bar keeps equalling decreasing nunber
                customerPB.Value = customerInc;

                //if given order or happiness progress bar empty, then walk off
                if (customerPB.Value == customerPB.Minimum || this.customerOrderCoffee.condiments.Count == 0)
                {
                    walkBack = true;

                    customerScore = customerPB.Value;
                    GameManager.Score += customerScore;

                    //reset the value
                    customerInc = 300;

                    //hide the controls when walking back
                    customerPB.Hide();
                }
            }
        }

        //======================================
        //PICTUREBOX
        //======================================

        public virtual void SetPictureBox(Bitmap image)
        {
            customerImg.Image = image;
        }

        public PictureBox GetPictureBox()
        {
            return customerImg;
        }

        //======================================
        //LOCATION OF COMPONENTS
        //======================================

        private void Location()
        {
            customerPB.Location = new Point(customerImg.Location.X, customerImg.Location.Y - 10);
        }

        //======================================
        //MOVEMENT OF COMPONENTS
        //======================================

        private void moveCustomer()
        {
            //go through the entire cords to check for free spaces for customer to line up
            for (int i = 0; i < Coordinates.GetInstance().Count; i++)
            {
                //if the customer is in a specified cord spot
                if (Coordinates.GetInstance()[i].Item1 == customerImg.Location)
                {
                    if (walkBack == true)
                    {
                        for (int j = i; j >= 0; j--)
                        {
                            isMoving = true;

                            //turn this spot to true becuase no cust is using it anymore
                            Coordinates.GetInstance()[i] = new Tuple<Point, bool>(new Point(Coordinates.GetInstance()[i].Item1.X, Coordinates.GetInstance()[i].Item1.Y), true);

                            if (j > 0)
                            {
                                //store dest in local var
                                destination = Coordinates.GetInstance()[j - 1].Item1;
                            }

                            else if (customerImg.Location == Coordinates.GetInstance()[0].Item1)
                            {
                                this.inUse = false;
                            }
                        }
                    }

                    else
                    {
                        if (i + 1 != Coordinates.GetInstance().Count)
                        {
                            //and if the next spot is free
                            if (Coordinates.GetInstance()[i + 1].Item2 == true)
                            {
                                //enable the customer to move
                                isMoving = true;

                                //turn the next stop to false so other customer know its not free
                                Coordinates.GetInstance()[i + 1] = new Tuple<Point, bool>(new Point(Coordinates.GetInstance()[i + 1].Item1.X, Coordinates.GetInstance()[i + 1].Item1.Y), false);

                                //turn this spot to true becuase no cust is using it anymore
                                Coordinates.GetInstance()[i] = new Tuple<Point, bool>(new Point(Coordinates.GetInstance()[i].Item1.X, Coordinates.GetInstance()[i].Item1.Y), true);

                                //store dest in local var
                                destination = Coordinates.GetInstance()[i + 1].Item1;
                            }
                        }
                    }
                }

                //if the cust is on the move
                if (isMoving == true)
                {
                    moveToNextPoint();
                    //jump out the loop, we dont need to iterate anymore
                    return;
                }

            }
        }

        private void moveToNextPoint()
        {
            //check where the customer needs to go to get to dest
            if (customerImg.Location.X < destination.X)
            {
                customerImg.Location = new Point(customerImg.Location.X + 1, customerImg.Location.Y);
            }

            else if (customerImg.Location.X > destination.X)
            {
                customerImg.Location = new Point(customerImg.Location.X - 1, customerImg.Location.Y);
            }

            if (customerImg.Location.Y < destination.Y)
            {
                customerImg.Location = new Point(customerImg.Location.X, customerImg.Location.Y + 1);
            }

            else if (customerImg.Location.Y > destination.Y)
            {
                customerImg.Location = new Point(customerImg.Location.X, customerImg.Location.Y - 1);
            }

            //if the cust has reached its dest, tell it to stop
            if (destination == customerImg.Location)
            {
                isMoving = false;
            }
        }

        //======================================
        //CUSTOMER ORDER
        //======================================

        public void GiveOrder()
        {
            walkBack = true;

            for (int i = 0; i < customerOrderCoffee.condiments.Count; i++)
            {
                customerOrderCoffee.condiments.RemoveAt(i);
            }
        }

        public void setCustomerOrder(Builder builder, Coffee coffee)
        {
            customerOrderCoffee = coffee;
        }

        public Coffee getCustomerOrder()
        {
            return customerOrderCoffee;
        }
    }
}
