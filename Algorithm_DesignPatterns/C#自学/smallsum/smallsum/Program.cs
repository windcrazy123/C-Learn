using System;

namespace smallsum
{
    class Program
    {
        static void Main(string[] args)
        {
            int small_sum;
            int[] vs = { 4, 2, 7, 2, 8 };
            SmallSum smallSum = new SmallSum();
            small_sum = smallSum.smallSum(vs);
            Console.WriteLine(small_sum);
        }
    }
    class SmallSum
    {
        public int smallSum(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return 0;
            }
            return process(arr, 0, arr.Length - 1);
        }
        private int process(int[] arr, int l, int r)
        {
            if (l == r)
            {
                return 0;
            }
            int mid = l + ((r - l) >> 1);
            return process(arr, l, mid)
            + process(arr, mid + 1, r)//T(N)=2*T(N/2)+O(N);a=2 b=2 d=1 logba=d;O(N*logN)
            + merge(arr, l, mid, r);
        }
        private int merge(int[] arr, int l, int m, int r)
        {
            int[] help = new int[r - l + 1];
            int i = 0;
            int p1 = l;
            int p2 = m + 1;
            int res = 0;
            while (p1 <= m && p2 <= r)
            {
                res += arr[p1] < arr[p2] ? (r - p2 + 1) * arr[p1] : 0;
                help[i++] = arr[p1] < arr[p2] ? arr[p1++] : arr[p2++];
            }
            while (p1 <= m)
            {
                help[i++] = arr[p1++];
            }
            while (p2 <= r)
            {
                help[i++] = arr[p2++];
            }
            for (i = 0; i < help.Length; i++)
            {
                arr[l + i] = help[i];
            }
            return res;
        }
    }
}
