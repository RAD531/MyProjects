using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    class Program
    {
        static void Main(string[] args)
        {
            /*LinearQueue<string> linearQueue = new LinearQueue<string>();

            linearQueue.offer("Ryan");
            linearQueue.offer("Bob");
            linearQueue.offer("Daniel");

            linearQueue.peek();

            foreach(string name in linearQueue)
            {
                Console.WriteLine(name);
            }

            linearQueue.poll();


            Console.WriteLine();

            foreach (string name in linearQueue)
            {
                Console.WriteLine(name);
            }

            CircularQueue q = new CircularQueue(5);

            q.enQueue(1);
            q.enQueue(2);
            q.enQueue(3);
            q.enQueue(4);
            q.enQueue(5);

            q.displayQueue();

            q.deQueue();
            q.deQueue();

            q.displayQueue();

            q.enQueue(6);
            q.enQueue(7);

            q.displayQueue();

            Console.ReadLine();*/

            PriorityQueue<int> priorityQueue = new PriorityQueue<int>();

            priorityQueue.add(33, 22, 33);
            priorityQueue.add(2, 34, 533);
            priorityQueue.add(7, 222, 33333);
            priorityQueue.add(444, 3, 1);
            priorityQueue.add(23, 333333, 3);
            priorityQueue.add(10,333,6);

            priorityQueue.remove(23);
            priorityQueue.add(2333, 434, 34);
           
            for(int i = 0; i < priorityQueue.size(); i++)
            {
                Console.WriteLine(priorityQueue.peek().ToString());
                priorityQueue.remove(priorityQueue.peek());
            }

            Console.ReadLine();

        }
    }
}
