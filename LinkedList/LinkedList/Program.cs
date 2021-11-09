using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            doublyLinkedList<string> doublyLinkedList = new doublyLinkedList<string>();

            doublyLinkedList.add("name1");
            doublyLinkedList.add("name3");
            doublyLinkedList.add("name4");

            doublyLinkedList.addAt(1, "name2");

            doublyLinkedList.addFirst("name0");

            doublyLinkedList.removeAt(4);

            Console.WriteLine(doublyLinkedList.Size());

            Console.WriteLine(doublyLinkedList.ToString());

            Console.ReadLine();

        }
    }
}
