﻿using System;

namespace 大堆
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 4, 8, 3, 2, 8, 9, 7, 53, 5, 8, };
            HeapSort heapSort = new HeapSort();
            heapSort.heapSort(arr);
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }
    }
    class HeapSort
    {
        public void heapSort(int[] arr)
        {
            if (arr==null||arr.Length<2)
            {
                return;
            }
            //for (int i = 0; i < arr.Length; i++)//O(N)
            //{
            //    heapInsert(arr, i);//O(logN)
            //}
            for (int i = arr.Length-1; i >= 0; i--)
            {
                heapify(arr, i, arr.Length);
            }
            int heapSize = arr.Length;
            swap(arr, 0, --heapSize);
            while (heapSize > 0)//O(N)
            {
                heapify(arr, 0, heapSize);//O(logN)
                swap(arr, 0, --heapSize);//O(1)
            }
        }

        private void heapInsert(int[] arr,int index)
        {
            while (arr[index]>arr[(index-1)/2])
            {
                swap(arr, index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        private void heapify(int[] arr,int index,int heapSize)
        {
            int left = index * 2 + 1;//左孩子的下标
            while (left<heapSize)//下方还有孩子的时候
            {//两个孩子中，谁的值大，把下标给largest
                int largest = left + 1 < heapSize && arr[left + 1] > arr[left] ? left + 1 : left;
                //父和较大的孩子之间，谁的值大，把下标给largest
                largest = arr[largest] > arr[index] ? largest : index;
                if (largest==index)
                {
                    break;
                }
                swap(arr, largest, index);
                index = largest;
                left = index * 2 + 1;
            }
        }

        private void swap(int[] arr,int i,int j)
        {
            int tmp = arr[i];
            arr[i] = arr[j];
            arr[j] = tmp;
        }
    }
}
