using UnityEngine;
using System.Collections;

public class Utilities
{
    public static void ArraySwap<T>(T[] arr, int a, int b)
    {
        if (arr.Length > 1)
        {
            T temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
    }

    public static void ArrayShuffle<T>(T[] arr, int times = 100)
    {
        for (int i = 0; i < times; i++)
        {

            int b = Random.Range(1, arr.Length);
            ArraySwap<T>(arr, 0, b);
        }
    }

    public class ppList<T>
    {
        int maxLength = 5;
        T[] arr;

        public ppList(int _maxLength = 5)
        {
            arr = new T[0];
            maxLength = _maxLength;
        }

        public int Length() { return arr.Length; }

        public int MaxLength() { return maxLength; }

        public void MaxLength(int _length) { maxLength = _length; }

        public T popFront()
        {
            T ret = arr[0];
            T[] temp = new T[arr.Length - 1];

            for (int i = 1; i < arr.Length; i++)
            {
                temp[i - 1] = arr[i];
            }

            arr = temp;

            return ret;
        }

        public void pushBack(T item)
        {
            if (arr.Length == maxLength)
            {
                popFront();
            }
            T[] temp = new T[arr.Length + 1];

            for (int i = 0; i < arr.Length; i++)
            {
                temp[i] = arr[i];
            }
            temp[arr.Length] = item;
            arr = temp;
        }

        public void Forget()
        {
            while (arr.Length > 0)
            {
                popFront();
            }
        }
        public T Index(int index)
        {
            return arr[index];
        }
    }
}
