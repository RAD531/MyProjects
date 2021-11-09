using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSortRecursion
{
    class Program
    {
        public static int OperationsMergeSort = 0;
        public static int finalOperationsMergeSort = 0;
        public static int OperationsMerge = 0;

        static void Main(string[] args)
        {
            List<int> unsorted = new List<int>();
            List<int> sorted;

            Random random = new Random();

            Console.WriteLine("Original array elements:");
            for (int i = 0; i < 1000000; i++)
            {
                unsorted.Add(random.Next(0, 100));
                //Console.Write(unsorted[i] + " ");
            }

            Console.WriteLine();

            sorted = MergeSort(unsorted);

            /*foreach (int i in sorted)
            {
                Console.Write(i + " ");
            }*/

            Console.Write("\n");

            Console.WriteLine();

            Console.WriteLine("Sorted array elements: ");
            Console.WriteLine("Number of layers = " + finalOperationsMergeSort.ToString());
            Console.WriteLine("Number of Merge sort operations = " + OperationsMergeSort.ToString());
            Console.WriteLine("Number of Merge operations = " + OperationsMerge.ToString());

            int total = OperationsMergeSort + OperationsMerge;

            Console.WriteLine("Total = " + total.ToString());

            Console.ReadLine();

        }

        //O (n * log n) or O(n log n)
        private static List<int> MergeSort(List<int> unsorted)
        {
            OperationsMergeSort++;

            //O(log n)
            //if list is less than 1, then it doesnt need sorting, it already is
            if (unsorted.Count <= 1)
            {
                if (finalOperationsMergeSort == 0)
                {
                    finalOperationsMergeSort = OperationsMergeSort - 1;
                }

                return unsorted;
            }


            //create lists for left and right to middle
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            int middle = unsorted.Count / 2;

            //add all elements of unsorted to left before middle
            for (int i = 0; i < middle; i++)
            {
                left.Add(unsorted[i]);
            }

            //add all elements of unsorted to right after middle
            for (int i = middle; i < unsorted.Count; i++)
            {
                right.Add(unsorted[i]);
            }

            //apply the half way split to the left list
            left = MergeSort(left);

            //apply the half way split to the right list
            right = MergeSort(right);

            //O(n)
            //sort the left and right lists
            return Merge(left, right);

        }

        private static List<int> Merge(List<int> left, List<int> right)
        {

            List<int> result = new List<int>();

            //keep looping until left and right are empty
            while(left.Count > 0 || right.Count > 0)
            {
                OperationsMerge++;

                //dont apply merge if both are empty
                if (left.Count > 0 && right.Count > 0)
                {
                    //compare the first elements of both left and right
                    //O(1)
                    if (left.First() <= right.First())
                    {
                        //add the left to the result list
                        result.Add(left.First());
                        ///remove the left from left list
                        left.Remove(left.First());
                    }

                    else
                    {
                        //add the right to the result list
                        result.Add(right.First());
                        //remove the right from the right list
                        right.Remove(right.First());
                    }
                }

                //if the left is greater than 0 but not the right
                else if (left.Count > 0)
                {
                    //the left will only have one more element than right
                    //this is due to half way splitting either even
                    //or odd number
                    result.Add(left.First());
                    left.Remove(left.First());
                }

                //if the right is greater than 0 but not the right
                else if (right.Count > 0)
                {
                    //the right will only have one more element than left
                    //this is due to half way splitting either even
                    //or odd number
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }

            //return sorted left and right lists
            return result;
        }
    }
}
