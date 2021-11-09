using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    class CircularQueue
    {
        //decalre the class vars
        private int size, front, rear;

        //declaring array list of integer type
        DynamicArray<int> queue = new DynamicArray<int>();

        public CircularQueue(int size)
        {
            this.size = size;
            this.front = this.rear = -1;
        }

        //method to insert a new element in the queue
        public void enQueue(int data)
        {
            //condition if queue is full
            if ((front == 0 && rear == size - 1) || (rear == (front -1) % (size - 1)))
            {
                throw new Exception("Queue is full");
            }

            else if (front == -1)
            {
                front = 0;
                rear = 0;
                queue.add(data);
            }

            else if (rear == size - 1 && front != 0)
            {
                rear = 0;
                queue.set(rear,data);
            }

            else
            {
                rear++;

                //adding a new element if
                if (front <= rear)
                {
                    queue.add(data);
                }

                //esle updating a old value
                else
                {
                    queue.set(rear, data);
                }

            }
        }

        //function to dequeue an element 
        //form the queue
        public int deQueue()
        {
            int temp;

            //condition for empty queue
            if (front == -1)
            {
                throw new Exception("Queue is Empty");
            }

            temp = queue.get(front);

            //condition for only one element
            if (front == rear)
            {
                front = -1;
                rear = -1;
            }

            else if (front == size - 1)
            {
                front = 0;
            }

            else
            {
                front++;
            }

            return temp;

        }

        //method to display the elements of queue
        public void displayQueue()
        {
            //condition for empty queue
            if (front == -1)
            {
                throw new Exception("queue is empty");
            }

            //if rear has not crossed the max size
            //or queue rear is still greater than front
            Console.WriteLine("Elements in the circular queue are: ");

            if (rear >= front)
            {
                //loop to print elements from front to rear
                for (int i = front; i <= rear; i++)
                {
                    Console.WriteLine(queue.get(i).ToString());
                    Console.WriteLine(" ");
                }
            }

            //if rear has crossed the max index and indexing has started in loop
            else
            {
                //loop for printing elements from front to max size or last index
                for (int i = front; i < size; i++)
                {
                    Console.WriteLine(queue.get(i).ToString());
                    Console.WriteLine(" ");
                }

                //loop for printing elements from 0th index till rear position
                for (int i = 0; i <= rear; i++)
                {
                    Console.WriteLine(queue.get(i).ToString());
                    Console.WriteLine(" ");
                }
            }
        }
    }
}
