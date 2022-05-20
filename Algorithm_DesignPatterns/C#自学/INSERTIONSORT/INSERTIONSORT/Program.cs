using System;

namespace INSERTIONSORT
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vs = { 3, 7, 2, 8, 35, 1, 9, 2 };
            InsertionSort insertionSort = new InsertionSort();
            insertionSort.insertionsort(vs);
            //foreach 循环
            foreach (var item in vs)
            {
                Console.WriteLine(item);
            }
            //数组转化为字符串
            Console.WriteLine(string.Join(",", vs));
            //Array.ForEach<T>(T[] array, Action < T > action); 方法
            Array.ForEach<int>(vs, (int i) => Console.WriteLine(i));
        }
    }

    //时间复杂度O(n^2) 如：最糟糕情况1+2+...+n=n(n+1)/2
    //空间复杂度O(1) (in place)
    class InsertionSort
    {
        public void insertionsort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int cur = array[i];
                int insertionindex = i - 1;
                //若(没有比到最后一个&&前面的数比被扫描的数cur大)
                //就将前面大的数放到后面(原理:下标+1)
                //最后将下一轮用的下标-1(相当于被扫描的数前移)
                while (insertionindex>=0 && array[insertionindex]>cur)
                {
                    array[insertionindex + 1] = array[insertionindex];
                    insertionindex--;
                }
                //将被扫描的数cur放在比他小的数后面
                array[insertionindex + 1] = cur;
            }
        }
    }
}
