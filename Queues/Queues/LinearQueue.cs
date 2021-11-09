using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    class LinearQueue<T> : IEnumerable<T>
    {
        private LinkedList<T> list = new LinkedList<T>();

        public LinearQueue()
        {

        }

        public LinearQueue(T firstElem)
        {
            offer(firstElem);
        }

        //return the size of the queue
        public int size()
        {
            return list.Count();
        }

        //returns whether or not the queue is empty
        public bool isEmpty()
        {
            return list.Count() == 0;
        }

        //peek the element at the front of the queue
        //the method throws an error if the queue is empty
        public T peek()
        {
            if (isEmpty())
            {
                throw new Exception("Queue empty");
            }

            return list.First();
        }

        //poll an element from the front of the queue
        //the method throws an error if the queue is emty

        public LinkedList<T> poll()
        {
            if (isEmpty())
            {
                throw new Exception("Queue empty");
            }

            list.RemoveFirst();

            return list;
        }

        //add an element to the back of the queue
        public void offer(T elem)
        {
            list.AddLast(elem);
        }


        //return an iterator to allow the user to traverse
        //through the elements found in the queue
        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

    }
}
