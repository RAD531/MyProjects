using System;

namespace LinearSearch
{
    class Program
    {
        static void Main(string[] args)
        {

            int total = 10000;
            int number = 9999;
            int [] numbersArray = new int[total];
            int operations = 0;


            for (int i = 0; i < total; i++)
            {

                numbersArray[i] = i;

            }


            foreach (int search in numbersArray)
            {
                operations += 1;

                if (number == search)
                {
                    Console.WriteLine("The number " + number + " has been found");
                    Console.WriteLine("The number of operations is " + operations.ToString());
                    break;
                }
            }

        }
    }
}
