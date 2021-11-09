using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    class DynamicArray<T> : IEnumerable<T> 
    {
        private T[] arr; //reference to array
        private int len = 0; //length user thinks array is
        private int capacity = 0; // actual array size
        int position = -1;

        public T Current => throw new NotImplementedException();


        public DynamicArray() : this(16)
        {
        }

        public DynamicArray(int capacity)
        {
            //shouldnt try to implement array which is negative
            if (capacity < 0)
            {
                throw new ArgumentException("Illegal Capacity: " + capacity.ToString());
            }

            //array eqauls new array with capacity
            else
            {
                this.capacity = capacity;
                arr = new T[capacity];
            }
        }

        //return size
        public int size()
        {
            return len;
        }

        //return if the array is empty
        public bool isEmpty()
        {
            return size() == 0;
        }

        //get value at element
        public T get(int index)
        {
            //check if index given is inside the bounds of array
            if (index >= len || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            //return value at index
            else
            {
                return arr[index];
            }
        }

        //set new value at index given
        public void set(int index, T elem)
        {
            //check if index is inside bounds of array
            if (index >= len || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            //set new value
            else
            {
                arr[index] = elem;
            }
        }

        //clear the array
        public void clear()
        {
            for (int i = 0; i < len; i++)
            {
                arr[i] = default;
            }

            //reset len to 0
            len = 0;
        }

        //add value at end of array
        public void add(T elem)
        {
            //Time to resize!
            if (len + 1 >= capacity)
            {
                if (capacity == 0)
                {
                    capacity = 1;
                }

                else
                {
                    capacity *= 2; //double to the size
                }

                T[] new_arr = new T[capacity];

                for (int i = 0; i < len; i++)
                {
                    new_arr[i] = arr[1]; //arr has extra nulls padded
                }

                arr = new_arr;
            }

            arr[len++] = elem;
        }

        //remove at index
        public T removeAt(int rm_index)
        {
            //check if index is inside bounds of array
            if (rm_index >= len || rm_index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            //need to restrucutre array, once item is deleted, need to push down other values unless value is at end
            else
            {
                T data = arr[rm_index];
                T[] new_arr = new T[len - 1];
                for (int i = 0, j = 0; i < len; i++, j++)
                {
                    if (i == rm_index)
                    {
                        j--; // skip over rm_index by fixing j temporarily
                    }

                    else
                    {
                        new_arr[j] = arr[i];
                    }
                }

                arr = new_arr;
                capacity = --len;
                return data;
            }
        }

        //remove where value is equal to value/s in array
        public bool remove(Object obj)
        {
            int index = indexOf(obj);

            if (index == -1)
            {
                return false;
            }

            removeAt(index);
            return true;
        }

        //get the index number where value equals index
        public int indexOf(Object obj)
        {
            for (int i = 0; i < len; i++)
            {
                if (obj == null)
                {
                    if (arr[i] == null)
                    {
                        return i;
                    }
                }

                else
                {
                    if (obj.Equals(arr[i]))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public bool contains(Object obj)
        {
            return indexOf(obj) != -1;
        }

        //send values back as string
        public override string ToString()
        {
            if (len == 0)
            {
                return "[]";
            }

            else
            {
                StringBuilder sb = new StringBuilder(len).Append("[");
                for (int i = 0; i < len - 1; i++)
                {
                    sb.Append(arr[i] + ", ");
                }

                return sb.Append(arr[len - 1] + "]").ToString();
            }
        }

        //move enumerator to next pos
        public bool MoveNext()
        {
            position++;
            return (position < len);
        }

        //reset enumerator to behind starting point in array
        public void Reset()
        {
            position = -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return arr.GetEnumerator();
        }
    }
}
