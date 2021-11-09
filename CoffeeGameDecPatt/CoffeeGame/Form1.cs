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
        //used for stroing added row in var
        private int addRow;

        //Coffee var to make coffees and add to ready DGV
        private Coffee coffee;

        //used to store reference to selected coffee in DGV and fill in condiments 
        private Coffee tempCoffee;

        //use new question object to create questions
        private Question question = new Question();

        //need to determine if question answer was correct
        private bool isCorrect = false;

        //reference to create new customers
        private ConcreteCustomer newCustomer;

        //need to store customers in list to manage them
        private List<Customer> customers = new List<Customer>();

        //need to hold coffees that need to be made
        private List<Coffee> coffeesReadyToBeMade = new List<Coffee>();

        //need to keep a value so progress bar can increment and sync with value
        private int coffeePBInc;

        public Form1()
        {
            InitializeComponent();

            //set the interval
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

                //create new customer object and add to list
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
            //update score
            lblScore.Text = "Score: " + GameManager.Score.ToString();

            //update DGV with cust items
            UpdateCustomerDGV();

            //check for cust that need disposing
            CheckCustomerDisposal();

            //update the condiments foreach ready order
            UpdateCondiments();

            //start the progress bar when items are being prepared
            ProgressBarValue();

        }

        private void orderStart(bool correct, Coffee coffee)
        {
            //if correct answer to question, add to list to be prepared
            if (correct == true)
            {
                coffeesReadyToBeMade.Add(coffee);
            }

            //else, display incorrect message to user
            else
            {
                MessageBox.Show("Incorrect answer, try again");
                GameManager.Score -= 300;
            }
        }

        private void ProgressBarValue()
        {
            //dont increase progress bar if there are no coffees ready to be made
            if (coffeesReadyToBeMade.Count > 0)
            {
                //only increase if progresss bar is less or equal to inc value
                if (pbOrderMaker.Value <= coffeePBInc)
                {
                    //increase number with each iteration
                    coffeePBInc++;
                    //progress bar keeps equalling increasing nunber
                    pbOrderMaker.Value = coffeePBInc;

                    //if progress bar full
                    if (pbOrderMaker.Value == pbOrderMaker.Maximum)
                    {
                        //reset the value
                        coffeePBInc = 0;
                        //reset progress bar
                        pbOrderMaker.Value = coffeePBInc;

                        //add coffee to inventory list
                        FillList(DGVReadyCoffees, coffeesReadyToBeMade.First());

                        //remove it from coffees ready to be made
                        coffeesReadyToBeMade.Remove(coffeesReadyToBeMade.First());
                    }
                }
            }

        }

        private void UpdateCondiments()
        {
            //fill in condiments on second cell from object in first cell
            for (int i = 0; i < DGVReadyCoffees.Rows.Count; i++)
            {
                tempCoffee = (Coffee)DGVReadyCoffees.Rows[i].Cells[0].Value;
                List<string> tempCoffeeList = new List<string>();

                DGVReadyCoffees.Rows[i].Cells[1].Value = tempCoffee.coffeeName;

                foreach (Condiment condiment in tempCoffee.condiments)
                {
                    tempCoffeeList.Add(condiment.condimentName);
                }

                //join the values, seperated by comma
                DGVReadyCoffees.Rows[i].Cells[2].Value = string.Join(",", tempCoffeeList);
            }
        }

        private void CheckCustomerDisposal()
        {
            //check if customer object can be disposed of
            if (customers.Count > 0)
            {
                for (int i = 0; i < customers.Count; i++)
                {
                    //if the customer has been marked for disposal, remove from list and dispose
                    if (customers[i].inUse == false)
                    {
                        customers[i] = null;
                        customers.RemoveAt(i);
                    }
                }
            }
        }

        //update the customer DGV in sync with the customer items static vars
        private void UpdateCustomerDGV()
        {
            //dont update if null
            if (CustItems.customerID != null)
            {
                DGVCustItems.AllowUserToAddRows = true;
                DGVCustItems.Rows[0].Cells[0].Value = CustItems.customerID;
                DGVCustItems.Rows[0].Cells[1].Value = CustItems.custCoffee;

                DGVCustItems.Rows[0].Cells[2].Value = CustItems.custCoffee.coffeeName;

                List<string> tempCoffeeList = new List<string>();

                foreach (Condiment condiment in CustItems.custCoffee.condiments)
                {
                    tempCoffeeList.Add(condiment.condimentName);
                }

                DGVCustItems.Rows[0].Cells[3].Value = string.Join(",", tempCoffeeList);
            }

            else
            {
                DGVCustItems.AllowUserToAddRows = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxMap.SendToBack();
            
            //create the customers on load
            createCustomer();

            //this defualt row must not be added if no items exist in the placeholder table
            //otherwise form basket will contain one empty cell with no data in
            DGVCustItems.AllowUserToAddRows = false;
            DGVReadyCoffees.AllowUserToAddRows = false;
        }

        //fill list of passed in datagridview
        public void FillList(DataGridView dataGridView, Coffee coffees)
        {
            addRow = dataGridView.Rows.Add();
            dataGridView.Rows[addRow].Cells[0].Value = coffees;
        }

        //add condiments to items already in made coffees
        public void UpdateCoffeeInList(Coffee coffee)
        {
            foreach(DataGridViewRow row in DGVReadyCoffees.Rows)
            {
                if (row.Selected == true)
                {
                    if (DGVReadyCoffees.Rows[row.Index].Cells[0].Value != null)
                    {
                        DGVReadyCoffees.Rows[row.Index].Cells[1].Value = coffee.coffeeName;
                    }

                    else
                    {
                        MessageBox.Show("You must add a coffee to the inventory first");
                    }
                }
            }
        }

        //click events for each coffee button
        //it adds the coffee or condiment to the coffee using dec pattern
        //asks the user for an answer to randomly generated question, store if they were correct or not in bool
        //start prepearing the coffee, function will check if answer was correct
        //if not correct, order wont be made, if correct, order starts
        private void btnLatte_Click(object sender, EventArgs e)
        {
            coffee = new Latte();
            isCorrect = question.GenerateQuestion();
            orderStart(isCorrect, coffee);
        }

        private void btnMocha_Click(object sender, EventArgs e)
        {
            coffee = new Mocha();
            isCorrect = question.GenerateQuestion();
            orderStart(isCorrect, coffee);
        }

        private void btnMacchiato_Click(object sender, EventArgs e)
        {
            coffee = new Macchiato();
            isCorrect = question.GenerateQuestion();
            orderStart(isCorrect, coffee);
        }

        private void btnCappuccino_Click(object sender, EventArgs e)
        {
            coffee = new Cappuccino();
            isCorrect = question.GenerateQuestion();
            orderStart(isCorrect, coffee);
        }

        private void btnAmericano_Click(object sender, EventArgs e)
        {
            coffee = new Americano();
            isCorrect = question.GenerateQuestion();
            orderStart(isCorrect, coffee);
        }

        //for condiments, they can added without asking question, they get added to existing ready made coffee
        private void btnMilk_Click(object sender, EventArgs e)
        {
            coffee = new Milk(coffee);
            UpdateCoffeeInList(coffee);
        }

        private void btnCaramel_Click(object sender, EventArgs e)
        {
            coffee = new Caramel(coffee);
            UpdateCoffeeInList(coffee);
        }

        private void btnChoc_Click(object sender, EventArgs e)
        {
            coffee = new Chocolate(coffee);
            UpdateCoffeeInList(coffee);
        }

        private void btnSugar_Click(object sender, EventArgs e)
        {
            coffee = new Sugar(coffee);
            UpdateCoffeeInList(coffee);

        }

        private void btnExpresso_Click(object sender, EventArgs e)
        {
            coffee = new Expresso();
            isCorrect = question.GenerateQuestion();
            orderStart(isCorrect, coffee);
        }

        private void DGVCustItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool haveOrder = false;

            // for loop to check every row for add button
            for (int i = DGVCustItems.Rows.Count - 1; i >= 0; i--)
            {
                //if cell 3 (add button) for any row clicked
                if ((bool)DGVCustItems.Rows[i].Cells[4].Selected)
                {
                    //go through every ready coffee list and check if it has the coffee type the customer wants
                    for (int foundItem = 0; foundItem < DGVReadyCoffees.Rows.Count; foundItem++)
                    {
                        //check if column coffee type is not null before storing in var 
                        if (DGVReadyCoffees.Rows[foundItem].Cells[0].Value != null)
                        {
                            //store the found item value in var
                            Coffee foundItemValue = (Coffee)DGVReadyCoffees.Rows[foundItem].Cells[0].Value;

                            //check if the found item is the coffee the customer wants
                            if (foundItemValue.coffeeName == CustItems.custCoffee.coffeeName)
                            {
                                //next check if the condiment column is null 
                                if (DGVReadyCoffees.Rows[foundItem].Cells[2].Value != null)
                                {
                                    //if not store the condiments in list
                                    List<Condiment> condimentsInList = new List<Condiment>();
                                    condimentsInList = foundItemValue.condiments;

                                    //go through the condiment list the customer wants
                                    foreach (Condiment custItem in CustItems.custCoffee.condiments)
                                    {
                                        //check if the first item in the condiment list is equal to any of the condiments prepared for the found coffee type
                                        for (int condimentInListItem = 0; condimentInListItem < condimentsInList.Count; condimentInListItem++)
                                        {
                                            //if it matches, then remove item from var list
                                            if (custItem.condimentName == condimentsInList[condimentInListItem].condimentName)
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

        private void DGVReadyCoffees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //update the coffee var when user selects new row
        //used to compare coffee to customer request
        private void DGVReadyCoffees_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView gv = sender as DataGridView;
            
            if(gv != null && gv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = gv.SelectedRows[0];

                if (row.Cells[0].Value != null)
                {
                    coffee = (Coffee)row.Cells[0].Value;
                }
            }
        }
    }
}
