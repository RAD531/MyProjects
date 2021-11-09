using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big0Tests
{
    class Program
    {
        public static int value = 1000;

        public static int[] collection = new int[value];

        public static int[] collection2 = new int[value];

        public static int operations;

        static void Main(string[] args)
        {
            for (int i = 0; i < value; i++)
            {
                collection[i] = i;
                collection[i] = value + 1;
            }

            Console.WriteLine(dublicates(collection, collection2));
            Console.WriteLine("operations " + operations.ToString());
            Console.ReadLine();
        }

        static bool dublicates(int[] collectionA, int[] collectionB)
        {

            //O(n)
            for (int i = 0; i < collectionA.Length; i++)
            {
                //O(n)
                for (int j = 0; j < collectionB.Length; j++)
                {
                    operations++;

                    if (collectionA[i] == collectionB[j])
                    {
                        return true;
                    }
                }
            }



            //O(a * b)

            return false;
        }
    }
}
