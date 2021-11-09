using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeGame
{
    class CustomMessageBox : System.Windows.Forms.Form
    {
        //add label for title to form
        Label message = new Label();
        //create possible locations for buttons to go
        List<Vector> PossibleButtonPos = new List<Vector>();
        //keep track of num of answer buttons requested from calling code
        List<Button> numOfButtons = new List<Button>();
        //need a button var to temp store button clicked
        Button tempbutton = new Button();
        //create an ok button to return dialog
        Button okButton = new Button();
        //store the user answer
        private string answer;

        public CustomMessageBox(string title, string body)
        {
            //default to none for answer if user doesnt select anything
            answer = "none";

            //define client size and add question title
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Text = title;

            //add possible locations
            PossibleButtonPos.Add(new Vector(10, 100));
            PossibleButtonPos.Add(new Vector(120, 100));
            PossibleButtonPos.Add(new Vector(240, 100));
            PossibleButtonPos.Add(new Vector(360, 100));

            //define prop for ok button and click event
            okButton.Location = new Point(10, 220);
            okButton.Click += new EventHandler(btn_ok_Click);
            okButton.Size = new System.Drawing.Size(100, 20);
            okButton.Text = "Confirm";
            this.Controls.Add(okButton);

            //add message location
            message.Location = new System.Drawing.Point(10, 10);
            message.Text = body;
            message.Font = Control.DefaultFont;
            message.AutoSize = true;

            //define further properties
            this.BackColor = Color.White;
            this.ShowIcon = false;
            this.Controls.Add(message);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            //if no answer was selected, ask user to select an answer
            if (answer == "none")
            {
                MessageBox.Show("Please select an answer");
            }

            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        public void AddButton(string buttonHeaderText)
        {
            //form cant go past four buttons, throw new exception
            if (numOfButtons.Count > 4)
            {
                throw new Exception("Can't exceed four buttons for custom message box");
            }

            //if room for button in list, add it to form
            else
            {
                Button button = new Button();
                numOfButtons.Add(button);

                //the button location will be the possible location index value
                button.Location = new System.Drawing.Point(PossibleButtonPos[numOfButtons.Count - 1].getSetX, PossibleButtonPos[numOfButtons.Count - 1].getSetY);
                button.Size = new System.Drawing.Size(100, 100);
                button.Text = buttonHeaderText;
                button.BackColor = Control.DefaultBackColor;
                this.Controls.Add(button);

                //create new event handler for same event
                button.Click += new EventHandler(btn_Click);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            //get the answer
            tempbutton = (Button)sender;
            answer = tempbutton.Text;
        }

        public string GetAnswer()
        {
            //return the answer
            return answer;
        }
    }
}
