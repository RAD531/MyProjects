using System;

namespace BinarySearch
{
    class Program
    {
        static void Main(string[] args)
        {

            var watch = new System.Diagnostics.Stopwatch();
            int total = 0;
            Console.WriteLine("Place range in array");
            total = Convert.ToInt32(Console.ReadLine());
            int[] numbers = new int[total];
            int min = 0;
            int max = numbers.Length;
            int guesses = 0;
            int number = 1;

            for(int i = 0; i < total; i++)
            {
                numbers[i] = i;
            }

            watch.Start();

            while (min <= max)
            {
                guesses += 1;

                int middle = (min + max) / 2;

                if (number == middle)
                {
                    Console.WriteLine("Found Match " + "Number of guesses = " + guesses);
                    watch.Stop();
                    Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                    break;
                }

                else if (number < numbers[middle])
                {
                    max = middle - 1;
                }

                else if (number > numbers[middle])
                {
                    min = middle + 1;
                }

            }
            
        }
    }
}
