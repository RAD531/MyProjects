using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathFinding
{
    class PriorityQueue<T>
    {
        //code gathered from https://www.dotnetlovers.com/article/231/priority-queue

        //create internal node class
        //make priority and object public
        class Node
        {
            public int Priority { get; set; }
            public T Object { get; set; }
        }

        //create list of objects, whatever data type they may be
        List<Node> queue = new List<Node>();
        //start the heap size as -1 so it sits at 0 with an enqueue
        //it can also be determined if the queue is empty with -1
        int heapSize = -1;
        //determine if the queue is going to priortise for the largest or smallest number in queue
        bool _isMinPriorityQueue;
        //return the count of the queue is needed
        public int Count { get { return queue.Count; } }

        /// <summary>
        /// If min queue or max queue
        /// </summary>
        /// <param name="isMinPriorityQueue"></param>
        public PriorityQueue(bool isMinPriorityQueue = false)
        {
            //if set to true, priority will go to the min number
            _isMinPriorityQueue = isMinPriorityQueue;
        }

        
        //O(log(n))
        public void Enqueue(int priority, T obj)
        {
            //create the node and set the priority, depending on min or max, the user gets to decide what the priority is
            Node node = new Node() { Priority = priority, Object = obj };
            //add the node to the queue
            queue.Add(node);
            //increase the heap size by 1
            heapSize++;
            //Maintaining heap
            //depedning on if min or max, build and reorganise the queue based on priority
            if (_isMinPriorityQueue)
                BuildHeapMin(heapSize);
            else
                BuildHeapMax(heapSize);
        }

        //O(log(n))
        public T Dequeue()
        {
            //dont dequeue if queue is empty
            if (heapSize > -1)
            {
                //get the value sitting at the highest priority
                var returnVal = queue[0].Object;
                //value at 0 index will equal value with index heap size/last value
                queue[0] = queue[heapSize];
                //remove the last value
                queue.RemoveAt(heapSize);
                //decrease the heap size by 1
                heapSize--;
                //Maintaining lowest or highest at root based on min or max queue
                if (_isMinPriorityQueue)
                    MinHeapify(0);
                else
                    MaxHeapify(0);
                return returnVal;
            }
            else
                throw new Exception("Queue is empty");
        }

        //give the user the option to update the priority with the object and index
        public void UpdatePriority(T obj, int priority)
        {
            int i = 0;

            //this for loop will take linear time, perhaps this can be improved, atm O(n)
            for (; i <= heapSize; i++)
            {
                //node equals index in queue
                Node node = queue[i];

                //if the node given equals the node in queue
                if (object.ReferenceEquals(node.Object, obj))
                {
                    //update the priority of the node
                    node.Priority = priority;

                    //no matter the size of the queue, the buildheapmin and min heapify will perform at O(log(n))
                    //the build heap min method simply keeps halfing the index value and compares priority values, swaps if needed until it reaches adequete pos
                    //update the queue prority for either min or max
                    if (_isMinPriorityQueue)
                    {
                        BuildHeapMin(i);
                        MinHeapify(i);
                    }
                    else
                    {
                        BuildHeapMax(i);
                        MaxHeapify(i);
                    }
                }
            }
        }

        //O(n)
        //give the user the option to search the queue with a given object
        public bool IsInQueue(T obj)
        {
            //look for the object in queue and return true if found
            //otherwise, return false
            foreach (Node node in queue)
                if (object.ReferenceEquals(node.Object, obj))
                    return true;
            return false;
        }

        private void BuildHeapMax(int i)
        {
            //need to place the max priorty at pos 0, we dont care about the rest of the queue values
            //just keep comparing index pos with half of the index pos until we get to pos 0
            while (i >= 0 && queue[(i - 1) / 2].Priority < queue[i].Priority)
            {
                Swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        /// <summary>
        /// Maintain min heap
        /// </summary>
        /// <param name="i"></param>
        private void BuildHeapMin(int i)
        {
            //need to place the min priorty at pos 0, we dont care about the rest of the queue values
            //just keep comparing index pos with half of the index pos until we get to pos 0
            while (i >= 0 && queue[(i - 1) / 2].Priority > queue[i].Priority)
            {
                Swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        private void MaxHeapify(int i)
        {
            int left = ChildL(i);
            int right = ChildR(i);

            int highest = i;

            if (left <= heapSize && queue[highest].Priority < queue[left].Priority)
                highest = left;
            if (right <= heapSize && queue[highest].Priority < queue[right].Priority)
                highest = right;

            if (highest != i)
            {
                Swap(highest, i);
                MaxHeapify(highest);
            }
        }

        //if we consider the p queue as a tree, each node can have child nodes depending on the size of the queue
        //the tree format can be used to organise the queue with priortising numbers
        private void MinHeapify(int i)
        {
            //get the left and right child of index number
            int left = ChildL(i);
            int right = ChildR(i);

            //store the lowest number
            int lowest = i;

            //lowest eqauls the lowest number out of the parent element and its children
            if (left <= heapSize && queue[lowest].Priority > queue[left].Priority)
                lowest = left;
            if (right <= heapSize && queue[lowest].Priority > queue[right].Priority)
                lowest = right;

            //if the lowest is not the parent, then swap them around
            if (lowest != i)
            {
                Swap(lowest, i);

                //apply direct recursion to start the process again of organising by priority 
                MinHeapify(lowest);
            }
        }

        //O(1)
        //perform a swap operation
        private void Swap(int i, int j)
        {
            var temp = queue[i];
            queue[i] = queue[j];
            queue[j] = temp;
        }

        //O(1)
        //get the left child of a parent
        private int ChildL(int i)
        {
            return i * 2 + 1;
        }

        //O(1)
        //get the right child of a parent
        private int ChildR(int i)
        {
            return i * 2 + 2;
        }

        //O(1)
        //peek the element with the highest priority
        public T peek()
        {
            return queue[0].Object;
        }
    }
}
