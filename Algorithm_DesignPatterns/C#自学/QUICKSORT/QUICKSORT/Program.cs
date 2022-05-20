using System;

namespace QUICKSORT
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vs = { 4, 2, 68, 32, 7, 45, 6, 5, 3, 64 };
            QuickSort quickSort = new QuickSort();
            quickSort.quickSort(vs);
            foreach (var item in vs)
            {
                Console.WriteLine(item);
            }
        }
    }

    /*
    /*时间复杂度：O(n^2)，平均时间复杂度：O(nlogN）
    空间复杂度：O(n)，平均空间复杂度：O(logN)
    在最坏的情况下，如果元素一开始就是从大到小倒序排列的，那么我们每个元素都需要调换，时间复杂度就是O(n^2)。
    当正常情况下，我们不会总碰到这样的情况，假设我们每次都找到一个中间的基准数，那么我们需要切分logN次，
    每层的划分(Partition)是O(N)，平均时间复杂度就是O(nlogN)。
    空间的复杂度取决于递归的层数，最糟糕的情况我们需要O(N)层，一般情况下，我们认为平均时间复杂度是O(logN)
    class QuickSort
    {
        public void quickSort(int[] array, int left, int right)
        {
            if (left >= right) return;
            int partitionIndex = partition(array, left, right);
            quickSort(array, left, partitionIndex - 1);
            quickSort(array, partitionIndex + 1, right);
        }

        //拆分数组：小于pivot在左边，大于反之.
        public int partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int leftIndex = left;
            int rightIndex = right - 1;
            while (true)
            {
                while (leftIndex < right && array[leftIndex] <= pivot)
                {
                    leftIndex++;
                }
                while (rightIndex >= left && array[rightIndex] > pivot)
                {
                    rightIndex--;
                }
                if (leftIndex > rightIndex) break;
                swap(array, leftIndex, rightIndex);
            }
            swap(array, leftIndex, right);
            return leftIndex;
        }
        public void swap(int[] array, int left, int right)
        {
            int temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
    }
    */
    class QuickSort
    {
        public void quickSort(int[] arr)
        {
            if (arr==null||arr.Length<2)
            {
                return;
            }
            quickSort(arr, 0, arr.Length - 1);
        }
        private static void quickSort(int[] arr,int L,int R)
        {
            if (L>R-60)
            {//插入排序
                for (int i = 1; i < arr.Length; i++)
                {
                    int cur = arr[i];
                    int insertionindex = i - 1;
                    //若(没有比到最后一个&&前面的数比被扫描的数cur大)
                    //就将前面大的数放到后面(原理:下标+1)
                    //最后将下一轮用的下标-1(相当于被扫描的数前移)
                    while (insertionindex >= 0 && arr[insertionindex] > cur)
                    {
                        arr[insertionindex + 1] = arr[insertionindex];
                        insertionindex--;
                    }
                    //将被扫描的数cur放在比他小的数后面
                    arr[insertionindex + 1] = cur;
                }
                return;
            }
            if (L < R)
            {
                swap(arr, L + (int)(random() * (R - L + 1)), R);
                int[] p = partition(arr, L, R);
                quickSort(arr, L, p[0] - 1);
                quickSort(arr, p[1] + 1, R);
            }
        }
        public static int[] partition(int[] arr,int L,int R)
        {
            int less = L - 1;
            int more = R;
            while (L<more)
            {
                if (arr[L]<arr[R])
                {
                    swap(arr, ++less, L++);
                }
                else if (arr[L]>arr[R])
                {
                    swap(arr, --more, L);
                }
                else
                {
                    L++;
                }
            }
            swap(arr, more, R);
            return new int[] { less + 1, more };
        }
        private static void swap(int[] array, int left, int right)
        {
            int temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
        private static double random()
        {
            Random random = new Random();
            int i = random.Next(0, 1000);
            double res = (double)i / 1000;
            return res;
        }
    }
}