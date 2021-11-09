using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    class PriorityQueue<T> : IComparable<T>
    {
        //source code from https://www.youtube.com/watch?v=GLIRnUhknP0 

        //dynamic list to track the elements inside the heap
        private ArrayList heap = null;

        //this map keeps track of the possible indices a particular
        //node value is found in the heap. Having this mapping lets
        //us have 0(log(n)) removals and 0(1) element containment check
        //at the cost of some additional space and minor overhead
        private Dictionary<T, SortedSet<int>> map = new Dictionary<T, SortedSet<int>>();

        //construct a priority queue with an inital capacity
        public PriorityQueue(int sz)
        {
            heap = new ArrayList(sz);
        }

        //construct and initially empty priority queue
        public PriorityQueue() : this(1)
        {

        }

        //construct a priority queue using heapify in 0(n) time
        public PriorityQueue(T[] elems)
        {
            int heapSize = elems.Length;
            heap = new ArrayList(heapSize);

            //place all element in heap
            for(int i = 0; i < heapSize; i++)
            {
                mapAdd(elems[i],i);
                heap.Add(elems[i]);

            }

            //heapify process 0(n)
            for (int i = Math.Max(0, (heapSize/2)-1); i>= 0; i++)
            {
                sink(i);
            }
        }

        //returns true/false depending on if the priority queue is empty
        public bool isEmpty()
        {
            return size() == 0;
        }

        //clears everything inside the heap, 0(n)
        public void clear()
        {
            heap.Clear();
            map.Clear();
        }

        //returns the size of the heap
        public int size()
        {
            return heap.Count;
        }

        //returns the value of the element with the lowest
        //priority in this priority queue. If the priority
        //queue is empty, null is returned
        public T peek()
        {
            if (heap.Count <= 0)
            {
                throw new Exception("Queue is empty");
            }

            return (T)heap[0];
        }

        //removes the root of the heap, 0(log(n))
        public T poll()
        {
            if (heap.Count == 0)
            {
                throw new Exception("heap is empty");
            }

            T temp = (T)heap[0];
            heap.RemoveAt(0);
            return temp;
        }

        //test if an element is in the heap, 0(1)
        public bool contains(T elem)
        {
            //map lookup to check containment, 0(1)
            if (elem == null)
            {
                return false;
            }

            return map.ContainsKey(elem);

            //linear scan to check containment, 0(n)
            /*for (int i = 0; i < heapSize; i++)
            {
                if (heap[i].Equals(elem))
                {
                    return true;
                }
            }

            return false*/
        }

        //adds an element to the priority queue, the
        //element must not be null, 0(log(n))
        public void add(T elem)
        {
            if (elem == null)
            {
                throw new Exception("Value to be added cannot be null");
            }

            heap.Add(elem);

            int indexOfLastElem = size() - 1;
            mapAdd(elem, indexOfLastElem);
            
            swim(indexOfLastElem);
        }

        //tests if the value of node i <= node j
        //this method assumes i & j are valid indices, 0(1)
        private bool less(int i, int j)
        {

            if ((int)heap[i] < (int)heap[j])
            {
                return true;
            }

            return false;
        }

        //bottom up node swim, 0(log(n))
        private void swim(int k)
        {
            //Grab th index of the next parent node WRT to k
            int parent = (k - 1) / 2;

            //keep swimming while we have not reached the 
            //root and while were less than our parent
            while(k > 0 && less(k, parent))
            {
                //exchange k with the parent
                swap(parent, k);
                k = parent;

                //grab the index of the next parent code WRT to k
                parent = (k - 1) / 2;
            }
        }

        //Top down node sink, 0(log(n))
        private void sink(int k)
        {
            int heapSize = size();

            while (true)
            {
                int left = 2 * k + 1; //left node
                int right = 2 * k + 2; //right node
                int smallest = left; //Assume left is the smallest node of the two children

                //find which is smaller left or right
                //if right is smaller, set smallest to be right
                if (right < heapSize && less(right, left))
                {
                    smallest = right;
                }

                //stop if were outside the bounds of the tree
                //or stop early if we cannot sink k anymore
                if (left >= heapSize || less(k, smallest))
                {
                    break;
                }

                //move down the tree following the smallest node
                swap(smallest, k);
                k = smallest;
            }
        }

        //swap to nodes, assumes i & j are valid, 0(1)
        private void swap(int i, int j)
        {
            T i_elem = (T)heap[i];
            T j_elem = (T)heap[j];

            heap[i] = j_elem;
            heap[j] = i_elem;

            mapSwap(i_elem, j_elem, i, j);
        }

        //removes a particular element in the heap, 0(login)
        public bool remove(T element)
        {
            if (element == null)
            {
                return false;
            }

            //linear removal via search 0(n)
            /*for (int i = 0; i < heapSize; i++)
            {
                if (element.Equals(heap[i]))
                {
                    heap.RemoveAt(i);
                    return true;
                }
            }*/

            //logarithmic removal with mao, 0(log(n))
            int index = mapGet(element);

            if (index != null)
            {
                removeAt(index);
            }

            return index != null;

        }

        private T removeAt(int i)
        {
            if (isEmpty())
            {
                throw new Exception("Value given cannot be null");
            }

            int indexOfLastElem = size() - 1;

            T removed_data = (T)heap[i];
            swap(i, indexOfLastElem);

            //obliterate the value
            heap.RemoveAt(indexOfLastElem);
            mapRemove(removed_data, indexOfLastElem);

            //Removed last element
            if (i == indexOfLastElem)
            {
                return removed_data;
            }

            T elem = (T)heap[i];

            //try sinking data
            sink(i);

            //if sinking did not work, try swimming
            if (heap[i].Equals(elem))
            {
                swim(i);
            }

            return removed_data;

        }

        //recursively checks if this heap is a min heap
        //this method is just for testing pruposes to make
        //sure the heap invariant is still being maintained
        //called this method wit k=0 to start at the root

        public bool isMinHeap(int k)
        {
            //if we are outside the bounds of the heap, return true

            int heapSize = size();

            if (k >= heapSize)
            {
                return true;
            }

            int left = 2 * k + 1;
            int right = 2 * k + 2;

            //make sure that the current node k is less than
            //both of its children left, and right if they exist
            //return false otherwise to indicate an invalid heap

            if (left < heapSize && !less(k, left))
            {
                return false;
            }

            if (right < heapSize && !less(k, right))
            {
                return false;
            }

            //recurse on both children to make sure they're also valid heaps
            return isMinHeap(left) && isMinHeap(right);
        }

        //add a node value and its index to the map
        private void mapAdd(T value, int index)
        {
            SortedSet<int> set;

            //value already exists in map
            try
            {
                set = map[value];
                set.Add(index);
            }

            //new value being inserted in map
            catch
            {
                set = new SortedSet<int>();
                set.Add(index);
                map.Add(value, set);
            }
        }

        //removes the index at a given value, 0(log(n))
        private void mapRemove(T value, int index)
        {
            SortedSet<int> set = map[value];
            set.Remove(index); //should be 0(log(n)) removal time
            
            if (set.Count() == 0)
            {
                map.Remove(value);
            }
        }

        //extract an index position for thr given value
        //NOTE: if a value exists multiple times in the heap, the
        //highest index is returned (this has arbitarily been chosen)
        private int mapGet(T value)
        {
            SortedSet<int> set = map[value];

            if (set == null)
            {
                throw new Exception("cant get value out of empty map");
            }

            return set.Last();
        }

        //exchange the index of two nodes internally within the map
        private void mapSwap(T val1, T val2, int val1Index, int val2Index)
        {
            SortedSet<int> set1 = map[val1];
            SortedSet<int> set2 = map[val2];

            set1.Remove(val1Index);
            set2.Remove(val2Index);

            set1.Add(val2Index);
            set2.Add(val1Index);
        }

        public override string ToString()
        {
            return heap.ToString();
        }

        public int CompareTo(T other)
        {
            throw new NotImplementedException();
        }
    }
}
