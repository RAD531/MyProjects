using System;
using System.Collections.Generic;

namespace BubbleSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int [] bubblesortarray = new int [] {1,4,50,1023,22,21,234,13,10,1,7,6,5,4,3,1 };
            int placeholder = 0;
            int numOfSorts = 1;
            int numOfPossibleSorts = bubblesortarray.Length * bubblesortarray.Length;
            List<int> numOfSortList = new List<int>();

            numOfSortList.Add(numOfSorts);

            for (int i = 0; i < bubblesortarray.Length - 1; i++)
            {

                for (int j = 0; j < bubblesortarray.Length - 1; j++)
                {

                    if (bubblesortarray[j] > bubblesortarray[j + 1])
                    {
                        placeholder = bubblesortarray[j + 1];
                        bubblesortarray[j + 1] = bubblesortarray[j];
                        bubblesortarray[j] = placeholder;
                    }
                }
            }

            Console.WriteLine("Sorted Numbers");

            for (int list = 0; list < bubblesortarray.Length; list++)
            {
                Console.WriteLine(bubblesortarray[list].ToString());
            }

            Console.WriteLine("Sorts compared to length of array = " + numOfSorts.ToString() + "/ " + numOfPossibleSorts.ToString());

            Console.WriteLine("----------------------");

            foreach(int sortlist in numOfSortList)
            {
                Console.WriteLine(sortlist.ToString());
            }
        }
    }
}
