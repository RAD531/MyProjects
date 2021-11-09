using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeGame
{
    public partial class Form1 : Form
    {
        private int addRow;

        //dependency
        private Coffees coffees;

        //association
        private Condiments condiments;

        private ConcreteCustomer newCustomer;

        //composition, objects will be destroyed when form object is destroyed through destructor
        private List<Customer> customers = new List<Customer>();

        public Form1()
        {
            InitializeComponent();

            GameManager.gameTimer.Interval = 10;
            //start the timer on start
            GameManager.gameTimer.Enabled = true;

            //event handler, every tick
            GameManager.gameTimer.Tick += gameTimer_Tick;
        }

        private async void createCustomer()
        {
            //loop through the num of customers assigned and create new objects
            for (int i = 0; i < 10; i++)
            {

                //alternative to thread sleep, which delays next line of code by a selected amount of time
                await Task.Delay(10000);

                newCustomer = new ConcreteCustomer();
                customers.Add(newCustomer);

                //set the properties and add controls
                this.Controls.Add(newCustomer.GetProgressBar());
                newCustomer.GetProgressBar().BringToFront();

                //finnally add the customer to screen
                this.Controls.Add(newCustomer.GetPictureBox());
                newCustomer.GetPictureBox().BringToFront();
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            lblScore.Text = "Score: " + GameManager.Score.ToString();

            if (CustItems.customerID != null)
            {
                DGVCustItems.AllowUserToAddRows = true;
                DGVCustItems.Rows[0].Cells[0].Value = CustItems.customerID;
                DGVCustItems.Rows[0].Cells[1].Value = CustItems.cofeeType;

                DGVCustItems.Rows[0].Cells[2].Value = string.Join(",", CustItems.condiments);
            }

            else
            {
                DGVCustItems.AllowUserToAddRows = false;
            }

            if (customers.Count > 0)
            {
                for (int i = 0; i < customers.Count; i++)
                {
                    if (customers[i].inUse == false)
                    {
                        customers[i] = null;
                        customers.RemoveAt(i);

                        if (customers.Count <= 0)
                        {
                            GameManager.gameTimer.Tick -= gameTimer_Tick;
                            MessageBox.Show("You have finished the game, your score was  " + GameManager.Score.ToString());
                            Application.Exit();
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxMap.SendToBack();

            createCustomer();

            //this defualt row must not be added if no items exist in the placeholder table
            //otherwise form basket will contain one empty cell with no data in
            DGVCustItems.AllowUserToAddRows = false;
            DGVReadyCoffees.AllowUserToAddRows = false;
        }

        public void FillList(DataGridView dataGridView, Coffees coffees)
        {
            addRow = dataGridView.Rows.Add();
            dataGridView.Rows[addRow].Cells[0].Value = coffees;
        }

        public void AddCondimentToDGV(string condiment)
        {
            foreach(DataGridViewRow row in DGVReadyCoffees.Rows)
            {
                if (row.Selected == true)
                {
                    if (DGVReadyCoffees.Rows[row.Index].Cells[0].Value != null)
                    {
                        if (DGVReadyCoffees.Rows[row.Index].Cells[1].Value != null)
                        {
                            DGVReadyCoffees.Rows[row.Index].Cells[1].Value += ", " + condiment;
                        }

                        else
                        {
                            DGVReadyCoffees.Rows[row.Index].Cells[1].Value += condiment;
                        }
                    }

                    else
                    {
                        MessageBox.Show("You must add a coffee to the inventory first");
                    }
                }
            }
        }

        private void btnLatte_Click(object sender, EventArgs e)
        {
            coffees = Coffees.Latte;
            FillList(DGVReadyCoffees, coffees);
        }

        private void btnMocha_Click(object sender, EventArgs e)
        {
            coffees = Coffees.Mocha;
            FillList(DGVReadyCoffees, coffees);
        }

        private void btnMacchiato_Click(object sender, EventArgs e)
        {
            coffees = Coffees.Macchiato;
            FillList(DGVReadyCoffees, coffees);
        }

        private void btnCappuccino_Click(object sender, EventArgs e)
        {
            coffees = Coffees.Cappuccino;
            FillList(DGVReadyCoffees, coffees);
        }

        private void btnAmericano_Click(object sender, EventArgs e)
        {
            coffees = Coffees.Americano;
            FillList(DGVReadyCoffees, coffees);
        }

        private void btnMilk_Click(object sender, EventArgs e)
        {
            condiments = Condiments.Milk;
            AddCondimentToDGV(condiments.ToString());
        }

        private void btnCaramel_Click(object sender, EventArgs e)
        {
            condiments = Condiments.Caramel;
            AddCondimentToDGV(condiments.ToString());
        }

        private void btnChoc_Click(object sender, EventArgs e)
        {
            condiments = Condiments.Chocolate;
            AddCondimentToDGV(condiments.ToString());
        }

        private void btnSugar_Click(object sender, EventArgs e)
        {
            condiments = Condiments.Sugar;
            AddCondimentToDGV(condiments.ToString());
        }

        private void btnExpresso_Click(object sender, EventArgs e)
        {
            coffees = Coffees.Expresso;
            FillList(DGVReadyCoffees, coffees);
        }

        private void DGVCustItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool haveOrder = false;

            // for loop to check every row for add button
            for (int i = DGVCustItems.Rows.Count - 1; i >= 0; i--)
            {
                //if cell 3 (add button) for any row clicked
                if ((bool)DGVCustItems.Rows[i].Cells[3].Selected)
                {
                    //go through every ready coffee list and check if it has the coffee type the customer wants
                    for (int foundItem = 0; foundItem < DGVReadyCoffees.Rows.Count; foundItem++)
                    {
                        //check if column coffee type is not null before storing in var 
                        if (DGVReadyCoffees.Rows[foundItem].Cells[0].Value != null)
                        {
                            //store the found item value in var
                            Coffees foundItemValue = (Coffees)DGVReadyCoffees.Rows[foundItem].Cells[0].Value;

                            //check if the found item is the coffee the customer wants
                            if (foundItemValue == CustItems.cofeeType)
                            {
                                //next check if the condiment column in null 
                                if (DGVReadyCoffees.Rows[foundItem].Cells[1].Value != null)
                                {
                                    //if not store the condiments in list
                                    List<string> condimentsInList = new List<string>();

                                    //if the cell contains more than one item, split and store in list
                                    if (DGVReadyCoffees.Rows[foundItem].Cells[1].Value.ToString().Contains(','))
                                    {
                                        condimentsInList = DGVReadyCoffees.Rows[foundItem].Cells[1].Value.ToString().Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                                    }

                                    //else, just store the one item
                                    else
                                    {
                                        condimentsInList.Add(DGVReadyCoffees.Rows[foundItem].Cells[1].Value.ToString());
                                    }

                                    //go through the condiment list the customer wants
                                    foreach (Condiments custItem in CustItems.condiments)
                                    {
                                        //check if the first item in the condiment list is equal to any of the condiments prepared for the found coffee type
                                        for (int condimentInListItem = 0; condimentInListItem < condimentsInList.Count; condimentInListItem++)
                                        {
                                            //if it matches, then remove item from var list
                                            if (custItem.ToString() == condimentsInList[condimentInListItem].ToString())
                                            {
                                                condimentsInList.RemoveAt(condimentInListItem);
                                                break;
                                            }
                                        }
                                    }

                                    //if var list equals 0, then we have the customer order
                                    if (condimentsInList.Count == 0)
                                    {
                                        haveOrder = true;
                                    }

                                    //break out of loop if we have the order
                                    if (haveOrder == true)
                                    {
                                        DGVReadyCoffees.Rows.RemoveAt(foundItem);
                                        break;
                                    }

                                }
                            }
                        }
                    }

                    //if the have order has not turned to true, then the order has not been made
                    if (haveOrder == false)
                    {
                        MessageBox.Show("You have not made the correct Coffee yet in your inventory");
                    }

                    //else have order = true, we have the order
                    else
                    {
                        //go through the customer list and only call the method of the right customer
                        foreach (Customer customer in customers.ToList())
                        {
                            //if we find the customer, give the order
                            if (DGVCustItems.Rows[i].Cells[0].Value == customer)
                            {
                                //turn the have order to true if we can find customer
                                haveOrder = true;
                                customer.GiveOrder();
                                customers.Remove(customer);
                                break;
                            }

                            //turn the have order to false if we cant find customer
                            else
                            {
                                haveOrder = false;
                            }
                        }

                        //if we cant find customer, then show error message
                        if (haveOrder == false)
                        {
                            MessageBox.Show("Could not find customer");
                        }
                    }
                }
            }
        }
    }
}
