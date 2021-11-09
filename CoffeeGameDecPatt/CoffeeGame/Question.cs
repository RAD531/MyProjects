using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeGame
{
    class Question
    {
        //store the user response to question in var was it correct or not?
        private bool correct = false;
        //generate random question
        private Random randomQuestion = new Random();
        //need to get the dialog result from custom message box i.e OK, close, cancel etc.
        private DialogResult dialogResult = new DialogResult();

        //create new tuple list to store many values for each item
        //this list will contain, int for id, string for question, list for possible answers, and string answer
        private List<Tuple<int, string, List<string>, string>> questions { get; set; } = new List<Tuple<int, string, List<string>, string>>();

        //create questions in constructor
        public Question()
        {
            questions.Add(new Tuple<int, string, List<string>, string>(1, "From which US city did Starbucks Originate?", new List<string> { "Seattle", "Boston", "San Francisco", "New Orleans" }, "Seattle"));

            questions.Add(new Tuple<int, string, List<string>, string>(2, "According to an Ethiopian origin story, coffee beans were discovered after what animal became energetic after eating them?", new List<string> { "Elephants", "Birds", "Goats", "Jaguars" }, "Goats"));

            questions.Add(new Tuple<int, string, List<string>, string>(3, "True or false: The world's most expensive coffee beans are harvested from poop.", new List<string> { "True", "False"}, "True"));

            questions.Add(new Tuple<int, string, List<string>, string>(4, "What is an espresso?", new List<string> { "A brewing method where water is forced through the coffee grounds", "A type of coffee bean that contains high amounts of caffeine", "A type of beverage made with whipped cream", "A type of bevergae made with steamed milk" }, "A brewing method where water is forced through the coffee grounds"));

            questions.Add(new Tuple<int, string, List<string>, string>(5, "Which ingredient is NOT found in a cappuccino?", new List<string> { "Espresso", "Steamed milk", "Whipped cream", "Milk foam" }, "Whipped cream"));

            questions.Add(new Tuple<int, string, List<string>, string>(6, "What is a latte?", new List<string> { "A drink made with only espresso", "A drink made with espresso and milk foam only", "A drink made with espresso and steamed milk", "A drink made with espresso and whipped cream only" }, "Whipped cream"));

            questions.Add(new Tuple<int, string, List<string>, string>(7, "What is a latte?", new List<string> { "A drink made with only espresso", "A drink made with espresso and milk foam only", "A drink made with espresso and steamed milk", "A drink made with espresso and whipped cream only" }, "A drink made with espresso and steamed milk"));

            questions.Add(new Tuple<int, string, List<string>, string>(8, "What's the difference between a cappuccino and a latte?", new List<string> { "Lattes contain less espresso and more milk", "Lattes contain more espresso and less milk", "Lattes and cappuccinos contain the same amount of espresso and milk, but lattes have less caffeine", "There is no difference" }, "Lattes contain less espresso and more milk"));

            questions.Add(new Tuple<int, string, List<string>, string>(9, "True or false: The darker the coffee bean, the more caffeine it contains.", new List<string> { "True", "False"}, "False"));

            questions.Add(new Tuple<int, string, List<string>, string>(10, "Which of these statements is true?", new List<string> { "Light roast beans are more acidic than medium and dark roast beans", "Light roast beans are more acidic than medium roast beans, but less acidic than dark roast beans", "Light roast beans are less acidic than medium roast beans, but more acidic than dark roast beans.", "Light roast beans are less acidic than medium and dark roast beans." }, "Light roast beans are more acidic than medium roast beans, but less acidic than dark roast beans"));

            questions.Add(new Tuple<int, string, List<string>, string>(11, "Where did the word 'frappuccino' originate?", new List<string> { "Italy", "Ethiopia", "Washington", "Massachusetts" }, "Massachusetts"));


            questions.Add(new Tuple<int, string, List<string>, string>(12, "What beverage is made by adding hot water to espresso?", new List<string> { "Cortado", "Americano", "Doppio", "Affogato" }, "Americano"));
        }

        //generate a question and send back to calling code
        public bool GenerateQuestion()
        {
            //need index to get random question
            int index = randomQuestion.Next(questions.Count);
            //get the question from index number
            var question = questions.ElementAt(index);

            //create new custom message box, pass in id and title
            CustomMessageBox customMessageBox = new CustomMessageBox(question.Item1.ToString(), (question.Item2));

            //add a answer button for each answer to question
            foreach(String qNumber in question.Item3)
            {
                customMessageBox.AddButton(qNumber);
            }

            //open the message box and get return value
            dialogResult = customMessageBox.ShowDialog();

            //if the ok button is clicked, it will close message box
            if (dialogResult == DialogResult.OK)
            {
                //get user answer, compare is agaisnt correct answer, turn the bool to true or false depending on user answer
                if(question.Item4 == customMessageBox.GetAnswer())
                {
                    correct = true;
                }

                else
                {
                    correct = false;
                }
            }

            //return if correct or not
            return correct;
        }
    }
}
