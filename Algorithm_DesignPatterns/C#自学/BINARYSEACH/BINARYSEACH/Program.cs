using System;

namespace BINARYSEACH
{
    class Program
    {
        static void Main()
        {
            int target_index;//输出目标所在数组中所在下标
            int[] array = { 1, 2, 3, 4, 5 };
            Seach seach = new Seach();
            target_index = seach.BinarySeach(array, 2);
            Console.WriteLine(target_index);
        }
    }
    //非递归
    class Seach
    {
        public int BinarySeach(int[] array, int target)
        {
            int left = 0;
            int right = array.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (array[mid] == target)
                {
                    return mid;
                }else if (target < array[mid])
                {
                    right = mid - 1;
                }else
                {
                    left = mid + 1;
                }
            }
            return -1;
        }
    }
}
    //Java递归方式
/*public int search(int nums[], int start, int end, int target)
{
    if (end >= start)
    {
        int mid = start + (end - start) / 2;
        if (nums[mid] == target)
        {
            return mid;
        }
        else if (target < nums[mid])
        {
            return search(nums, start, mid - 1, target);
        }
        return search(nums, mid + 1, end, target);
    }
    return -1;
}*/