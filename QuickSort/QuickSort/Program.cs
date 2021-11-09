using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] testnumbers = { 2, 3, 2, 44, 15, 22, 1, 777 };

            foreach (int number in testnumbers)
            {
                Console.WriteLine("numbers unsorted: " + number);
            }

            Console.WriteLine();

            testnumbers = quick_sort(testnumbers, 0, 7);

            foreach (int number in testnumbers)
            {
                Console.WriteLine("numbers sorted: " + number);
            }

            Console.ReadKey();
        }

        private static int[] quick_sort(int[] items, int start, int end)
        {
            //base case
            if (start >= end)
            {
                return null;
            }

            int pivot_value = items[start]; //set pivot value to first item in partition
            int low_mark = start + 1; //set to second value in partition
            int high_mark = end; //set to last value on partition
            bool finished = false;

            //repeat until low and high values have been swapped as needed 
            while (finished == false)
            {
                while (low_mark <= high_mark && items[low_mark] <= pivot_value)
                {
                    low_mark = low_mark + 1; //increment low mark
                }

                while (low_mark <= high_mark && items[high_mark] >= pivot_value)
                {
                    high_mark = high_mark - 1; //decrement highmark
                }

                //swap values
                if (low_mark < high_mark)
                {
                    int temp = items[low_mark];
                    items[low_mark] = items[high_mark];
                    items[high_mark] = temp;
                }

                else
                {
                    finished = true;
                }
            }

            //swap pivot value and value at highmark
            int temp2 = items[start];
            items[start] = items[high_mark];
            items[high_mark] = temp2;

            //recursvie call on left partition
            quick_sort(items, start, high_mark - 1);

            //recursvie call on right partition
            quick_sort(items, high_mark + 1, end);

            return items;
        }
    }
}
