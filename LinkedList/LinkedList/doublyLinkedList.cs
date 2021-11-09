using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class doublyLinkedList<T>
    {
        private int size = 0;
        private Node<T> head = null;
        private Node<T> tail = null;

        //empty this linked list, 0(n)
        public void clear()
        {
            Node<T> trav = head;

            while(trav != null)
            {
                Node<T> next = trav.next;
                trav.prev = trav.next = null;
                trav.data = default;
                trav = next;
            }

            head = tail = trav = null;
            size = 0;
        }

        //return the size of this linked list
        public int Size()
        {
            return size;
        }

        //is this linked list empty?
        public bool isEmpty()
        {
            return Size() == 0;
        }

        //add element to the tail of the linked list, 0(1)
        public void add(T elem)
        {
            addLast(elem);
        }

        //add a node to the tail of the linked list, 0(1)
        public void addLast(T elem)
        {
            if (isEmpty())
            {
                head = tail = new Node<T>(elem, null, null);
            }

            else
            {
                tail.next = new Node<T>(elem, tail, null);
                tail = tail.next;
            }

            size++;
        }

        //add an element to the beginning of this linked list, O(1)
        public void addFirst(T elem)
        {
            if (isEmpty())
            {
                head = tail = new Node<T>(elem, null, null);
            }

            else
            {
                head.prev = new Node<T>(elem, null, head);
                head = head.prev;
            }

            size++;
        }

        //add an element at a specified index
        public void addAt(int index, T data)
        {

            try
            {

                if (index < 0)
                {
                    throw new Exception("Illegal Index");
                }

                if (index == 0)
                {
                    addFirst(data);
                    return;
                }

                if (index == size)
                {
                    addLast(data);
                }

                Node<T> temp = head;

                for (int i = 0; i < index; i++)
                {
                    temp = temp.next;
                }

                Node<T> newNode = new Node<T>(data, temp, temp.next);

                temp.next.prev = newNode;
                temp.next = newNode;

                size++;

            }

            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        //check the value of the first node if it still exits, O(1)
        public T peekFirst()
        {
            if (isEmpty())
            {
                throw new Exception("Empty List");
            }

            return head.data;
        }

        //check the value of the last node if it exists, O(1)
        public T peekLast()
        {
            if (isEmpty())
            {
                throw new Exception("Empty List");
            }

            return tail.data;
        }

        //remove the first value at the head of the linked list, O(1)
        public T removeFirst()
        {
            //cant remove data from empty list
            if (isEmpty())
            {
                throw new Exception("Empty List");
            }

            //extract the data at the head and move
            //the head pointer forwards one node
            T data = head.data;
            head = head.next;
            --size;

            //if the list is empty, set the tail to null
            if (isEmpty())
            {
                tail = null;
            }

            //do a memory cleanup of the prev noe
            else
            {
                head.prev = null;
            }

            //return the data that was at the first node we just removed
            return data;
        }

        //remove the last valye at the tail of the linked list, O(1)
        public T removeLast()
        {
            //cant remove data from an empty list 
            if (isEmpty())
            {
                throw new Exception("Empty List");
            }

            //extract the data at the tail and move
            //the tail pointer backwards one node
            T data = tail.data;
            tail = tail.prev;
            --size;

            //if the list is now empty, set the head to null
            if (isEmpty())
            {
                head = null;
            }

            //do a memory clean of the node that was just removed 
            else
            {
                tail.next = null;
            }

            //return the data that was in the last node we just removed
            return data;
        }

        //remove an arbitrary node from the linked list, O(1)
        private T remove(Node<T> node)
        {
            //if the node to remove is somewhere either at the
            //head or the tail, handle those independently
            if (node.prev == null)
            {
                return removeFirst();
            }

            if (node.next == null)
            {
                return removeLast();
            }

            //make the pointers of adjacent nodes skip over 'node'
            node.next.prev = node.prev;
            node.prev.next = node.next;

            //temporarily store the data we want to return
            T data = node.data;

            //memory cleanup
            node.data = default;
            node = node.prev = node.next = null;

            --size;

            //return the data in the node we just removed
            return data;
        }

        //remove a node at a particular index, 0(n)
        public T removeAt(int index)
        {
            //make sure the index provided is valid
            if (index < 0 || index >= size)
            {
                throw new ArgumentException();
            }

            int i;
            Node<T> trav;

            //search from the front of the list
            if (index < size / 2)
            {
                for (i = 0, trav = head; i != index; i++)
                {
                    trav = trav.next;
                }
            }

            //search from back of list
            else
            {
                for (i = size - 1, trav = tail; i != index; i--)
                {
                    trav = trav.prev;
                }
            }

            return remove(trav);
        }

        //remove a particualr value in the linked list, O(n)
        public bool remove(Object obj)
        {
            Node<T> trav = head;

            //support searching for null
            if (obj == null)
            {
                for (trav = head; trav != null; trav = trav.next)
                {
                    if (trav.data == null)
                    {
                        remove(trav);
                        return true;
                    }
                }
            }

            //search for non null object
            else
            {
                for(trav = head; trav != null; trav = trav.next)
                {
                    if (obj.Equals(trav.data))
                    {
                        remove(trav);
                        return true;
                    }
                }
            }

            return false;
        }

        //find the index of a particular value in the linked list, O(n)
        public int indexOf(Object obj)
        {
            int index = 0;
            Node<T> trav = head;

            //support searching for null
            if (obj == null)
            {
                for (; trav != null; trav = trav.next, index++)
                {
                    if (trav.data == null)
                    {
                        return index;
                    }
                }
            }

            //search for non null object
            else
            {
                for(; trav != null; trav = trav.next, index++)
                {
                    if (obj.Equals(trav.data))
                    {
                        return index;
                    }
                }            
            }

            return -1;
        }
        //check if a value is contained within the linked list
        public bool contains(Object obj)
        {
            return indexOf(obj) != -1;
        }

        public bool hasNext()
        {
            Node<T> trav = head;

            return trav != null;
        }

        public T next()
        {
            Node<T> trav = head;

            T data = trav.data;
            trav = trav.next;
            return data;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            Node<T> trav = head;

            while(trav != null)
            {
                sb.Append(trav.data);

                if(trav.next != null)
                {
                    sb.Append(", ");
                }

                trav = trav.next;
            }

            sb.Append(" ]");
            return sb.ToString();
        }


    }
}
