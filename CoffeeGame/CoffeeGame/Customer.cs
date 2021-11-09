using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CoffeeGame
{
    abstract class Customer
    {
        private int progressBarValue;
        //used to decrease customer progress bar
        private int customerInc;
        //make the customer imgage accessible
        private PictureBox customerImg = new PictureBox();
        //same for progress bar
        private ProgressBar customerPB = new ProgressBar();

        //association
        private Coffees customerOrderCoffee { get; set; }

        //aggregation/multiplicity
        private List<Condiments> condiments = new List<Condiments>();
        private bool isMoving = false;

        //adds to user score, gets populated by final progress bar value
        private int customerScore = 0;

        private Point destination;
        //walk back identifies if the customer is walking away from the counter
        private bool walkBack = false;
        //let form 1 know when to dispose of object
        public bool inUse {get;set;}


        public Customer()
        {
            //set values
            this.progressBarValue = 3000;
            this.inUse = true;
            this.customerPB.Maximum = this.progressBarValue;
            this.customerPB.Value = this.progressBarValue;
            this.customerInc = this.progressBarValue;

            //start the customer timer
            GameManager.gameTimer.Tick += custTimer_Tick;

            //Set up event handlers
            customerImg.MouseClick += Customer_OnClick;

            //Call methods
            orderGenerator();
            drawCustomer();
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

        public void SetProgressBar(int PBValue)
        {
            progressBarValue = PBValue;
        }

        private void DrawProgressBar()
        {
            //set up the progress bar control with size, location and value
            customerPB.Visible = true;
            customerPB.Size = new Size(30, 10);
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
                if (customerPB.Value == customerPB.Minimum || this.condiments.Count == 0)
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

        private void DrawPictureBox()
        {
            customerImg.Location = new Point(Coordinates.coordinates[0].Item1.X, Coordinates.coordinates[0].Item1.Y);
            customerImg.Image = Properties.Resources.Cust;

            //make the picture box transparent
            customerImg.BackColor = Color.Transparent;
            //assign new size
            customerImg.Size = new Size(30, 30);
            //make the image fit to the picture box new size
            customerImg.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public virtual void SetPictureBox(Bitmap image)
        {
            customerImg.Image = image;
        }

        public PictureBox GetPictureBox()
        {
            return customerImg;
        }

        protected void Customer_OnClick(object sender, MouseEventArgs e)
        {
            if (walkBack == false)
            {
                CustItems.customerID = this;
                CustItems.cofeeType = customerOrderCoffee;

                CustItems.condiments.Clear();

                foreach (Condiments condiments in condiments)
                {
                    CustItems.condiments.Add(condiments);
                }
            }
        }

        //======================================
        //DRAW COMPONENTS
        //======================================

        private void drawCustomer()
        {
            DrawProgressBar();
            DrawPictureBox();
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
            for (int i = 0; i < Coordinates.coordinates.Count; i++)
            {
                //if the customer is in a specified cord spot
                if (Coordinates.coordinates[i].Item1 == customerImg.Location)
                {
                    if (walkBack == true)
                    {
                        for (int j = i; j >= 0; j--)
                        {
                            isMoving = true;

                            //turn this spot to true becuase no cust is using it anymore
                            Coordinates.coordinates[i] = new Tuple<Point, bool>(new Point(Coordinates.coordinates[i].Item1.X, Coordinates.coordinates[i].Item1.Y), true);

                            if (j > 0)
                            {
                                //store dest in local var
                                destination = Coordinates.coordinates[j - 1].Item1;
                            }

                            else if (customerImg.Location == Coordinates.coordinates[0].Item1)
                            {
                                this.inUse = false;
                            }
                        }
                    }

                    else
                    {
                        if (i + 1 != Coordinates.coordinates.Count)
                        {
                            //and if the next spot is free
                            if (Coordinates.coordinates[i + 1].Item2 == true)
                            {
                                //enable the customer to move
                                isMoving = true;

                                //turn the next stop to false so other customer know its not free
                                Coordinates.coordinates[i + 1] = new Tuple<Point, bool>(new Point(Coordinates.coordinates[i + 1].Item1.X, Coordinates.coordinates[i + 1].Item1.Y), false);

                                //turn this spot to true becuase no cust is using it anymore
                                Coordinates.coordinates[i] = new Tuple<Point, bool>(new Point(Coordinates.coordinates[i].Item1.X, Coordinates.coordinates[i].Item1.Y), true);

                                //store dest in local var
                                destination = Coordinates.coordinates[i + 1].Item1;
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

        protected void orderGenerator()
        {
            //set up new random for the number of items
            Random orderquan = new Random();
            //int to hold the maximum allowed items
            int maximumOrder = 0;

            //set up random for the order item
            Random order = new Random();

            //can order three items for level 3
            maximumOrder = orderquan.Next(1, 4);

            var CoffeeArray = Enum.GetValues(typeof(Coffees));
            var CondimentArray = Enum.GetValues(typeof(Condiments));

            // used to identify random selection of item from products list
            int j = 0;
            //select random item from products list
            j = order.Next(CoffeeArray.Length);
            customerOrderCoffee = (Coffees)j;

            //start loop to add required number of items
            for (int i = 0; i < maximumOrder; i++)
            {
                i = order.Next(CondimentArray.Length);
                this.condiments.Add((Condiments)i);
            }
        }

        public void GiveOrder()
        {
            walkBack = true;
            CustItems.customerID = null;
            CustItems.cofeeType = null;

            for (int i = 0; i < CustItems.condiments.Count; i++)
            {
                CustItems.condiments.RemoveAt(i);
            }

            for (int i = 0; i < condiments.Count; i++)
            {
                condiments.RemoveAt(i);
            }
        }


        //======================================
        //DISPOSE
        //======================================

        /*public void Dispose()
        {
            //stop this customer timer
            GameManager.gameTimer.Tick -= custTimer_Tick;

            customerImg.Dispose();
            customerImg = null;
            customerPB.Dispose();
            customerPB = null;
        }*/
    }
}
