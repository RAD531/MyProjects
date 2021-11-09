#define p
using System;

namespace MergeSort
{
    class Program
    {
           class Example
        {
            static public void merge(int[] arr, int p, int q, int r)
            {
                int i, j, k;
                int n1 = q - p + 1;
                int n2 = r - q;
                int[] L = new int[n1];
                int[] R = new int[n2];

                for (i = 0; i < n1; i++)
                {
                    L[i] = arr[p + i];
                }

                for (j = 0; j < n2; j++)
                {
                    R[j] = arr[q + 1 + j];
                }

                i = 0;
                j = 0;
                k = p;

                while (i < n1 && j < n2)
                {
                    if (L[i] <= R[j])
                    {
                        arr[k] = L[i];
                        i++;
                    }
                    else
                    {
                        arr[k] = R[j];
                        j++;
                    }
                    k++;
                }

                while (i < n1)
                {
                    arr[k] = L[i];
                    i++;
                    k++;
                }

                while (j < n2)
                {
                    arr[k] = R[j];
                    j++;
                    k++;
                }
            }

            static public void mergeSort(int[] arr, int p, int r)
            {
                if (p < r)
                {
                    int q = (p + r) / 2;
                    mergeSort(arr, p, q);
                    mergeSort(arr, q + 1, r);
                    merge(arr, p, q, r);
                }
            }

            static void Main(string[] args)
            {
                var watch = new System.Diagnostics.Stopwatch();
                int arrayTotal = 1000000;
                int[] arr = new int [arrayTotal];

                for (int j = 0; j < arrayTotal; j++)
                {

                    arr[j] = j;

                }

                int n = arrayTotal, i;
                Console.WriteLine("Merge Sort");
 
                watch.Start();
                mergeSort(arr, 0, n - 1);
                watch.Stop();

                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            }
        }
    }
}

