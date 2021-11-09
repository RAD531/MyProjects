using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_s_algorithm
{
    class Node<T>
    {
        public T data { get; set; }
        public Node<T> prev { get; set; }
        public Node<T> next { get; set; }


        public Node(T data, Node<T> prev, Node<T> next)
        {
            this.data = data;
            this.prev = prev;
            this.next = next;

        }

        public override string ToString()
        {
            string test = "hello";
            Console.WriteLine("test '{0}'", test);


            return data.ToString();
        }
    }
}
