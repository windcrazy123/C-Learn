using System;

namespace RADIXSORT
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 3, 74, 3, 7, 3, 7, 3, 7, 8 };
            Sort.RadixSort(arr);
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }
    }
    class Sort
    {
        public static void RadixSort(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }
            RadixSort(arr, 0, arr.Length - 1, maxbits(arr));
        }
        //计算最大数有几位
        private static int maxbits(int[] arr)
        {
            int max = Int32.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                max = Math.Max(max, arr[i]);
            }
            int res = 0;
            while (max != 0)
            {
                res++;
                max /= 10;//10进制计算
            }
            return res;
        }

        // arr[begin到end]排序
        private static void RadixSort(int[] arr, int L, int R, int digit)
        {
            const int radix = 10;
            int i = 0, j = 0;
            //准备辅助空间
            int[] bucket = new int[R - L + 1];
            for (int d = 1; d <= digit; d++)//有多少个十进制位就进出多少次
            {//10个桶0~9
             // count[0] 当前位（d位）是0的数字有多少个
             // count[1] 当前位（d位）是（0和1）的数字有多少个
             // count[i] 当前位（d位）是（0~i）的数字有多少个
                int[] count = new int[radix];
                for (i = L; i <= R; i++)
                {
                    j = GetDigit(arr[i], d);
                    count[j]++;
                }
                for (i = 1; i < radix; i++)
                {
                    count[i] = count[i] + count[i - 1];
                }

                //出桶从右到左，进入辅助数组
                for (i = R; i >= L; i--)
                {
                    j = GetDigit(arr[i], d);
                    bucket[count[j] - 1] = arr[i];
                    count[j]--;
                }
                //从辅助数组拷贝到原数组
                for (i = L, j = 0; i <= R; i++, j++)
                {
                    arr[i] = bucket[j];
                }
            }
        }
        private static int GetDigit(int x, int d)
        {
            return ((x / ((int)Math.Pow(10, d - 1))) % 10);
        }
    }
}
