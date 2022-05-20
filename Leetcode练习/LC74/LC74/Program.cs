using System;

namespace LC74
{
    class Program
    {
        static void Main(string[] args)
        {
            bool output;
            int[][] matrix = new int[0][];
            //matrix[0] = new int[1] { 1 };

            Solution solution = new Solution();
            output = solution.SearchMatrix(matrix, 0);
            Console.WriteLine(output);
        }
    }
    public class Solution
    {
        //没有考虑到matrix == null || matrix.length == 0的情况
        public bool SearchMatrix(int[][] matrix, int target)
        {
            bool key = false;
            int up = 0;
            int left = 0;
            int m = matrix.Length - 1;
            int n = matrix[0].Length - 1;
            while (up<=m)
            {
                int mid = up + (m - up) / 2;//可简化为:int mid = up + (m - up) >> 1 右移一位
                if (matrix[mid][0]==target)
                {
                    return !key;
                }else if (matrix[mid][0] < target)
                {
                    up = mid + 1;
                }
                else
                {
                    m = mid - 1;
                }
            }
            while (left<=n)
            {
                try
                {
                    int mi = left + (n - left) / 2;
                    if (matrix[m][mi]==target)
                    {
                        return !key;
                    }else if (matrix[m][mi] < target)
                    {
                        left = mi + 1;
                    }
                    else
                    {
                        n = mi - 1;
                    }
                }
                catch (Exception)
                {
                    break;
                }
                
            }
            return key;
        }
    }
}
//以下方法是将我的遗漏补充，且
//将两个while循环合并但比我的方法慢将近两倍，且
/*
 public boolean searchMatrix(int[][] matrix, int target) {
    if (matrix == null || matrix.length == 0) {
        return false;
    }
    int start = 0, rows = matrix.length, cols = matrix[0].length;
    int end = rows * cols - 1;
    while (start <= end) {
        int mid = (start + end) / 2;//不如我的方法好，因为start+end可能容易溢出
        if (matrix[mid / cols][mid % cols] == target) {
            return true;
        }
        if (matrix[mid / cols][mid % cols] < target) {
            start = mid + 1;
        } else {
            end = mid - 1;
        }
    }
    return false;
}
 */