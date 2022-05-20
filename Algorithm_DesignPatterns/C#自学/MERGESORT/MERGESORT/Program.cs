using System;

namespace MERGESORT
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vs = { 2, 1, 4, 7, 3 };
            MergeSort mergeSort = new MergeSort();
            mergeSort.mergeSort(vs);
            Console.WriteLine(string.Join(",",vs));
        }
    }
    class MergeSort
    {
        public void mergeSort(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }
            process(arr, 0, arr.Length - 1);
        }
        private void process(int[] arr, int L, int R)
        {
            if (L == R)
            {
                return;
            }
            int mid = L + ((R - L) >> 1);
            process(arr, L, mid);
            process(arr, mid + 1, R);//T(N)=2*T(N/2)+O(N);a=2 b=2 d=1 logba=d;O(N*logN)
            merge(arr, L, mid, R);
        }
        private void merge(int[] arr, int L, int M, int R)
        {
            int[] help = new int[R - L + 1];
            int i = 0;
            int p1 = L;
            int p2 = M + 1;
            while (p1 <= M && p2 <= R)
            {
                help[i++] = arr[p1] <= arr[p2] ? arr[p1++] : arr[p2++];
            }
            while (p1 <= M)
            {
                help[i++] = arr[p1++];
            }
            while (p2 <= R)
            {
                help[i++] = arr[p2++];
            }
            for (i = 0; i < help.Length; i++)
            {
                arr[L + i] = help[i];
            }
        }
    }
}
